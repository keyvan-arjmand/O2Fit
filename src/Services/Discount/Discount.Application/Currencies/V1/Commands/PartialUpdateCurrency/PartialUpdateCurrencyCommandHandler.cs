using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Domain.Aggregates.CurrencyAggregate;

namespace Discount.Application.Currencies.V1.Commands.PartialUpdateCurrency;

public class PartialUpdateCurrencyCommandHandler : IRequestHandler<PartialUpdateCurrencyCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public PartialUpdateCurrencyCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(PartialUpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Currency>.Filter.Eq(x => x.CurrencyCode, request.CurrencyCode);
        var currency = await _work.GenericRepository<Currency>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (currency == null) throw new NotFoundException("Currency not found");
        currency.CoefficientCurrency = request.CoefficientCurrency;
        await _work.GenericRepository<Currency>()
            .UpdateOneAsync(x => x.Id == currency.Id, currency,
                new Expression<Func<Domain.Aggregates.CurrencyAggregate.Currency, object>>[]
                {
                    x => x.CoefficientCurrency,
                }, null, cancellationToken);
        await _cacheService.DeleteKeyAsync(currency.CurrencyCode);
        await _cacheService.CacheResponseAsync(currency.CurrencyCode, currency, TimeSpan.FromMinutes(5));
    }
}