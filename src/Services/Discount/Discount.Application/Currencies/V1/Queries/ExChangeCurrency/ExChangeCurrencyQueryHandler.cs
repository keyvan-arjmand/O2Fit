using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.CurrencyAggregate;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Discount.Application.Currencies.V1.Queries.ExChangeCurrency;

public class ExChangeCurrencyQueryHandler : IRequestHandler<ExChangeCurrencyQuery, ExChangeResult>
{
    private readonly IResponseCacheService _cacheService;
    private readonly IUnitOfWork _work;

    public ExChangeCurrencyQueryHandler(IResponseCacheService cacheService, IUnitOfWork work)
    {
        _cacheService = cacheService;
        _work = work;
    }

    public async Task<ExChangeResult> Handle(ExChangeCurrencyQuery request, CancellationToken cancellationToken)
    {
        var cashSourceRedis = await _cacheService.GetCachedResponseAsync(request.SourceCurrencyCode);
        var cashTargetRedis = await _cacheService.GetCachedResponseAsync(request.DestinationCurrencyCode);
        if (string.IsNullOrWhiteSpace(cashSourceRedis) || string.IsNullOrWhiteSpace(cashTargetRedis))
        {
            var sourceFilter = Builders<Currency>.Filter.Eq(x => x.CurrencyCode, request.SourceCurrencyCode);
            var sourceCurrency = await _work.GenericRepository<Currency>()
                .GetSingleDocumentByFilterAsync(sourceFilter, cancellationToken);
            var targetFilter = Builders<Currency>.Filter.Eq(x => x.CurrencyCode, request.DestinationCurrencyCode);
            var targetCurrency = await _work.GenericRepository<Currency>()
                .GetSingleDocumentByFilterAsync(targetFilter, cancellationToken);

            return new ExChangeResult
            {
                DestinationCurrencyCode = targetCurrency.CurrencyCode,
                DestinationCurrencyAmount =
                    request.SourceCurrencyAmount.CurrencyExchanger(sourceCurrency.CoefficientCurrency,
                        targetCurrency.CoefficientCurrency),
                SourceCurrencyAmount = request.SourceCurrencyAmount,
                ExRate = targetCurrency.CoefficientCurrency,
                SourceCurrencyCode = sourceCurrency.CurrencyCode
            };
        }
        else
        {
            var sourceCurrency =
                JsonConvert.DeserializeObject<Currency>(cashSourceRedis);
            var targetCurrency =
                JsonConvert.DeserializeObject<Currency>(cashTargetRedis);
            return new ExChangeResult
            {
                DestinationCurrencyCode = targetCurrency.CurrencyCode,
                DestinationCurrencyAmount =
                    request.SourceCurrencyAmount.CurrencyExchanger(sourceCurrency.CoefficientCurrency,
                        targetCurrency.CoefficientCurrency),
                SourceCurrencyAmount = request.SourceCurrencyAmount,
                ExRate = targetCurrency.CoefficientCurrency,
                SourceCurrencyCode = sourceCurrency.CurrencyCode
            };
        }
    }
}