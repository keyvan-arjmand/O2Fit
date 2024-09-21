using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Domain.ValueObjects;

namespace Discount.Application.Currencies.V1.Commands.InsertCurrency;

public class InsertCurrencyCommandHandler:IRequestHandler<InsertCurrencyCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public InsertCurrencyCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(InsertCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = new Domain.Aggregates.CurrencyAggregate.Currency
        {
            CurrencyCode = new CurrencyCode(request.CurrencyCode),
            CountryIds = request.CountryIds,
            CoefficientCurrency = request.CoefficientCurrency,
        };
        await _work.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>().InsertOneAsync(currency, cancellationToken: cancellationToken);
        await _cacheService.DeleteKeyAsync(currency.CurrencyCode);
        await _cacheService.CacheResponseAsync(currency.CurrencyCode, currency, TimeSpan.FromMinutes(5));
    }
}