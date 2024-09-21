using Common.Constants.Currency;
using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Utilities;
using Currency.Application.Currencies.V1.Query.GetByCountryId;
using Currency.Application.Currencies.V1.Query.GetByNameCurrency;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Currency.Grpc.Services;

public class ExchangeCurrencyService : Exchangers.ExchangersBase
{
    private readonly RedisCache _redis;
    private readonly IMediator _mediator;

    public ExchangeCurrencyService(RedisCache redis, IMediator mediator)
    {
        _redis = redis;
        _mediator = mediator;
    }

    public override async Task<ExchangerResponse> ExchangerCurrencies(ExchangerRequest request,
        ServerCallContext context)
    {
        var cash = await _redis.GetStringAsync(RedisKeys.ExchangeCurrency);
        var exchange = JsonConvert.DeserializeObject<List<Domain.Aggregates.CurrencyAggregate.Currency>>(cash);

        var sourceCurrency = exchange?.FirstOrDefault(x => x.CurrencyName == request.SourceCurrencyName);
        if (sourceCurrency == null) throw new NotFoundException("sourceCurrency Not Found");
        var targetCurrency = exchange?.FirstOrDefault(x => x.CurrencyName == request.DestinationCurrencyName);
        if (targetCurrency == null) throw new NotFoundException("targetCurrency Not Found");
        return new ExchangerResponse
        {
            SourceCurrencyName = sourceCurrency?.CurrencyName,
            SourceCurrencyAmount = request.SourceCurrencyAmount,
            DestinationCurrencyName = targetCurrency?.CurrencyName,
            DestinationCurrencyAmount =
                request.SourceCurrencyAmount.CurrencyExchanger(sourceCurrency.CoefficientCurrency,
                    targetCurrency.CoefficientCurrency)
        };
    }

    public override async Task<ExchangerToDollarResponse> ExchangerCurrencyToDollar(ExchangerToDollarRequest request,
        ServerCallContext context)
    {
        var cash = await _redis.GetStringAsync(RedisKeys.ExchangeCurrency);
        var exchange = JsonConvert.DeserializeObject<List<Domain.Aggregates.CurrencyAggregate.Currency>>(cash);

        var sourceCurrency = exchange?.FirstOrDefault(x => x.CurrencyName == request.CurrencyName);
        if (sourceCurrency == null) throw new NotFoundException("sourceCurrency Not Found");
        var targetCurrency = exchange?.FirstOrDefault(x => x.Alpha == "USD");

        return new ExchangerToDollarResponse
        {
            SourceCurrencyName = sourceCurrency.CurrencyName,
            SourceCurrencyAmount = request.CurrencyAmount,
            DestinationCurrencyName = targetCurrency.CurrencyName,
            DestinationCurrencyAmount =
                request.CurrencyAmount.CurrencyExchangeToDollar(sourceCurrency.CoefficientCurrency)
        };
    }

    public override async Task<CurrencyByCountryIdResponse> GetCurrencyByCountryId(CurrencyByCountryIdRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetCurrencyByCountryIdQuery
        {
            CountryIds = request.CountryId
        });
        if (result == null) throw new NotFoundException("currency not found");
        return new CurrencyByCountryIdResponse
        {
            //CountryId = result.CountryIds,
            Alpha = result.Alpha,
            CurrencyName = result.CurrencyCode,
            CoefficientCurrency = result.CoefficientCurrency,
            CountryCode = result.CountryCode,
            CurrencyCode = result.CurrencyCode,
            Id = result.Id,
        };
    }

    public override async Task<CurrencyByNameResponse> GetCurrencyByName(CurrencyByNameRequest request,
        ServerCallContext context)
    {
        var currency = await _mediator.Send(new GetCurrencyByCodeQuery(request.Name));
        if (currency == null) throw new NotFoundException("currency not found");
        return new CurrencyByNameResponse
        {
            Id = currency.Id,
            CurrencyName = currency.CurrencyCode,
            CoefficientCurrency = currency.CoefficientCurrency,
            CurrencyCode = currency.CurrencyCode,
        };
    }
}