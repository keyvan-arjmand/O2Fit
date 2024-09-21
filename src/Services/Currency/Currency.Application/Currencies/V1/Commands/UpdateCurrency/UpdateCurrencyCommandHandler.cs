using Common.Constants.Currency;
using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Interfaces.Services;
using Currency.Application.Dtos;
using Currency.Domain.Aggregates.CurrencyAggregate;
using Currency.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Events.Services.Discount.Command.DiscountCurrencyRate;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace Currency.Application.Currencies.V1.Commands.UpdateCurrency;

public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _cacheService;


    public UpdateCurrencyCommandHandler(IUnitOfWork uow, IResponseCacheService cacheService)
    {
        _uow = uow;
        _cacheService = cacheService;
    }

    public async Task<string> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetByIdAsync(request.Id, cancellationToken); //Fill With Exchange Api
        if (currency == null) throw new NotFoundException("currency Not Found");
        currency.CoefficientCurrency = request.CoefficientCurrency;
        currency.CurrencyTranslationName.Arabic  = request.Name.Arabic;
        currency.CurrencyTranslationName.Persian = request.Name.Persian;
        currency.CurrencyTranslationName.Persian = request.Name.Persian;
        currency.CurrencyTranslationName.English =request.Name.English;
        
        currency.AddDomainEvent(new UpdateCurrencyRate(currency.Id, currency.CountryIds, currency.CurrencyCode,
            currency.CoefficientCurrency)); // for discount
        
        await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .UpdateOneAsync(x => x.Id == request.Id, currency,
                new Expression<Func<Domain.Aggregates.CurrencyAggregate.Currency, object>>[]
                {
                    x => x.CoefficientCurrency,
                    x => x.CurrencyTranslationName.Arabic ,
                    x => x.CurrencyTranslationName.Persian,
                    x => x.CurrencyTranslationName.English,
                }, null, cancellationToken);
        await _cacheService.DeleteKeyAsync(currency.CurrencyCode);
        await _cacheService.CacheResponseAsync(currency.CurrencyCode, currency, TimeSpan.FromMinutes(5));
        return currency.Id;
    }
}