using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.CurrencyAggregate;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Discount.Application.Currencies.V1.Queries.GetCurrencyById;

public class GetCurrencyByCodeQueryHandler : IRequestHandler<GetCurrencyByCodeQuery, Currency>
{
    private readonly IResponseCacheService _cacheService;
    private readonly IUnitOfWork _work;

    public GetCurrencyByCodeQueryHandler(IResponseCacheService cacheService, IUnitOfWork work)
    {
        _cacheService = cacheService;
        _work = work;
    }

    public async Task<Currency> Handle(GetCurrencyByCodeQuery request, CancellationToken cancellationToken)
    {
        var redis = await _cacheService.GetCachedResponseAsync(request.Code);
        if (string.IsNullOrWhiteSpace(redis))
        {
            var filter = Builders<Currency>.Filter.Eq(x => x.CurrencyCode, request.Code);
            var currency= await _work.GenericRepository<Currency>()
                .GetSingleDocumentByFilterAsync(filter, cancellationToken);
            if (currency==null)
                throw new NotFoundException($"Currency Code Not Valid {request.Code}");
         
            await _cacheService.CacheResponseAsync(currency.CurrencyCode, currency, TimeSpan.FromMinutes(5));
            return currency;
        }
        else
        {
            var currency =
                JsonConvert.DeserializeObject<Currency>(redis);
            return currency!;
        }
    }
}