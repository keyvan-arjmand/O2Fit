using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Application.Dtos;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Events.Services.Discount.Command.DiscountCurrencyRate;
using MassTransit;

namespace Discount.Application.Consumers.Currency;

public class UpdateCurrencyRateConsumer: IConsumer<UpdateCurrencyRate>
{
    private readonly IResponseCacheService _cacheService;
    private readonly IUnitOfWork _work;

    public UpdateCurrencyRateConsumer(IResponseCacheService cacheService, IUnitOfWork work)
    {
        _cacheService = cacheService;
        _work = work;
    }

    public async Task Consume(ConsumeContext<UpdateCurrencyRate> context)
    {
        var filter =
            Builders<Domain.Aggregates.CurrencyAggregate.Currency>.Filter.Eq(x => x.CurrencyCode,
                context.Message.CurrencyCode);
        var discountCurrency = await _work.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetSingleDocumentByFilterAsync(filter, context.CancellationToken);
        if (discountCurrency != null)
        {
            discountCurrency.CoefficientCurrency = context.Message.CoefficientCurrency;
            await _work.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
                .UpdateOneAsync(x => x.Id == discountCurrency.Id, discountCurrency,
                    new Expression<Func<Domain.Aggregates.CurrencyAggregate.Currency, object>>[]
                    {
                        x => x.CoefficientCurrency,
                    }, null, context.CancellationToken);
            await _cacheService.DeleteKeyAsync(context.Message.CurrencyCode);
            await _cacheService.CacheResponseAsync(context.Message.CurrencyCode, new Domain.Aggregates.CurrencyAggregate.Currency
            {
                CurrencyCode = discountCurrency.CurrencyCode,
                CoefficientCurrency = discountCurrency.CoefficientCurrency,
                CountryIds = discountCurrency.CountryIds,
                Id = discountCurrency.Id
            }, TimeSpan.FromMinutes(5));

        }
        else
        {
            await _work.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
                .InsertOneAsync(new Domain.Aggregates.CurrencyAggregate.Currency
                {
                    CurrencyCode = new CurrencyCode(context.Message.CurrencyCode),
                    CountryIds = context.Message.CountryIds,
                    CoefficientCurrency = context.Message.CoefficientCurrency,
                }, cancellationToken: context.CancellationToken);
            await _cacheService.CacheResponseAsync(context.Message.Id, new CurrencyResult
            {
                CurrencyCode = context.Message.CurrencyCode,
                CoefficientCurrency = context.Message.CoefficientCurrency,
                CountryIds = context.Message.CountryIds,
                Id = context.Message.Id
            }, TimeSpan.FromMinutes(5));

        }
    }
}