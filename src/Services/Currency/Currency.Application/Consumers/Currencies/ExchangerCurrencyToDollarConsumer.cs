using Common.Constants.Currency;
using Currency.Application.Common.Interfaces.Services;
using Currency.Application.Common.Utilities;
using Currency.Application.Currencies.V1.Query.GetByNameCurrency;
using Currency.Application.Dtos;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Currency.Application.Consumers.Currencies;

public class ExchangerCurrencyToDollarConsumer : IConsumer<ExchangerCurrencyToDollar>
{
    private readonly IMediator _mediator;
    private readonly IResponseCacheService _cacheService;

    public ExchangerCurrencyToDollarConsumer(IMediator mediator, IResponseCacheService cacheService)
    {
        _mediator = mediator;
        _cacheService = cacheService;
    }

    public async Task Consume(ConsumeContext<ExchangerCurrencyToDollar> context)
    {
        var cashSourceCurrency = await _cacheService.GetCachedResponseAsync(context.Message.CurrencyCode);
        var cashTargetCurrency = await _cacheService.GetCachedResponseAsync("USD");
        if (!string.IsNullOrWhiteSpace(cashSourceCurrency))
        {
            var sourceCurrency = JsonConvert.DeserializeObject<CurrencyDto>(cashSourceCurrency);
            var targetCurrency = JsonConvert.DeserializeObject<CurrencyDto>(cashTargetCurrency);
            await context.RespondAsync(new ExchangerCurrencyToDollarResult
            {
                SourceCurrencyCode = sourceCurrency.CurrencyCode,
                SourceCurrencyAmount = context.Message.CurrencyAmount,
                DestinationCurrencyCode = targetCurrency.CurrencyCode,
                DestinationCurrencyAmount =
                    context.Message.CurrencyAmount.CurrencyExchangeToDollar(sourceCurrency.CoefficientCurrency),
                ExRate = sourceCurrency.CoefficientCurrency,
            });
        }
        else
        {
            
            var sourceCurrency = await _mediator.Send(new GetCurrencyByCodeQuery(context.Message.CurrencyCode));
            var targetCurrency = await _mediator.Send(new GetCurrencyByCodeQuery("USD"));
            if (sourceCurrency != null || targetCurrency != null)
            {
                //redis
                await _cacheService.CacheResponseAsync(sourceCurrency.CurrencyCode, new CurrencyDto
                {
                    CurrencyCode = sourceCurrency.CurrencyCode,
                    CoefficientCurrency = sourceCurrency.CoefficientCurrency,
                    CountryIds = sourceCurrency.CountryIds,
                    CurrencyTranslationName = sourceCurrency.CurrencyTranslationName,
                    CountryCode = sourceCurrency.CountryCode,
                    Alpha = sourceCurrency.Alpha,
                    Id = sourceCurrency.Id
                }, TimeSpan.FromMinutes(5));
                await _cacheService.CacheResponseAsync(targetCurrency.CurrencyCode, new CurrencyDto
                {
                    CurrencyCode = targetCurrency.CurrencyCode,
                    CoefficientCurrency = targetCurrency.CoefficientCurrency,
                    CountryIds = targetCurrency.CountryIds,
                    CurrencyTranslationName = targetCurrency.CurrencyTranslationName,
                    CountryCode = targetCurrency.CountryCode,
                    Alpha = targetCurrency.Alpha,
                    Id = targetCurrency.Id
                }, TimeSpan.FromMinutes(5));
                
                await context.RespondAsync(new ExchangerCurrencyToDollarResult
                {
                    SourceCurrencyCode = sourceCurrency.CurrencyCode,
                    SourceCurrencyAmount = context.Message.CurrencyAmount,
                    DestinationCurrencyCode = targetCurrency.CurrencyCode,
                    DestinationCurrencyAmount =
                        context.Message.CurrencyAmount.CurrencyExchangeToDollar(sourceCurrency.CoefficientCurrency),
                    ExRate = sourceCurrency.CoefficientCurrency,
                });
              
            }
            else
            {
                await context.RespondAsync(new ExchangerCurrencyResult
                {
                    SourceCurrencyCode = string.Empty,
                    SourceCurrencyAmount = 0,
                    DestinationCurrencyCode = string.Empty,
                    DestinationCurrencyAmount = 0,
                    ExRate = 0
                });
            }
        }
    }
}