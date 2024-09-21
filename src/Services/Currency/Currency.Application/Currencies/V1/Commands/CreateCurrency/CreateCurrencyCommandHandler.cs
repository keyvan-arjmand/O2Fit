using Common.Constants.Currency;
using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Interfaces.Services;
using Currency.Application.Dtos;
using Currency.Domain.Aggregates.CurrencyAggregate;
using Currency.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Events.Services.Currency;
using EventBus.Messages.Events.Services.Discount.Command.DiscountCurrencyRate;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using MongoDB.Bson;

namespace Currency.Application.Currencies.V1.Commands.CreateCurrency;

public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _cacheService;
    private readonly IRequestClient<GetCountryByCurrencyCode> _client;

    public CreateCurrencyCommandHandler(IUnitOfWork uow, IRequestClient<GetCountryByCurrencyCode> client,
        IResponseCacheService cacheService)
    {
        _uow = uow;
        _client = client;
        _cacheService = cacheService;
    }

    public async Task Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var validCode = new CurrencyCode(request.CurrencyCode);
        if (await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
                .AnyAsync(x => x.CurrencyCode == new CurrencyCode(request.CurrencyCode), cancellationToken))
            throw new AppException($"Currency Already Exist:{request.CurrencyCode}");

        // var country = await _client.GetResponse<GetCountryByCurrencyCodeResult>(new GetCountryByCurrencyCode
        // {
        //     CurrencyCode = request.CurrencyCode
        // }, cancellationToken);
        // if (string.IsNullOrEmpty(country.Message.Id))
        //     throw new NotFoundException($"Currency Code Not Valid {request.CurrencyCode}");

        Domain.Aggregates.CurrencyAggregate.Currency currency = new Domain.Aggregates.CurrencyAggregate.Currency
        {
            CurrencyCode = validCode,
            CurrencyName = request.CurrencyName,
            CoefficientCurrency = request.CoefficientCurrency,
            CountryIds = request.CountryIds,
            CurrencyTranslationName = new Translation
            {
                Persian = request.Name.Persian,
                English = request.Name.English,
                Arabic = request.Name.Arabic,
            },
        };
        currency.AddDomainEvent(new PartialUpdatedCurrencyCode(currency.CurrencyCode,
            currency.CountryIds)); // for country
        currency.AddDomainEvent(new UpdateCurrencyRate(currency.Id, currency.CountryIds,
            currency.CurrencyCode,
            currency.CoefficientCurrency)); // for discount 
        await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>() 
            .InsertOneAsync(currency, null, cancellationToken);
        await _cacheService.CacheResponseAsync(currency.CurrencyCode, currency, TimeSpan.FromMinutes(5));
    }
}