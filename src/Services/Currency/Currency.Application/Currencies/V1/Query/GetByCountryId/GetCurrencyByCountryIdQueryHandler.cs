using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Mapping;
using Currency.Application.Dtos;
using MongoDB.Bson;

namespace Currency.Application.Currencies.V1.Query.GetByCountryId;

public class GetCurrencyByCountryIdQueryHandler : IRequestHandler<GetCurrencyByCountryIdQuery, CurrencyDto>
{
    private readonly IUnitOfWork _uow;

    public GetCurrencyByCountryIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CurrencyDto> Handle(GetCurrencyByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var countryIds = new List<int>(request.CountryIds);
        var filter = Builders<Domain.Aggregates.CurrencyAggregate.Currency>.Filter.Eq(x => x.CountryIds, countryIds);
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (currency == null) throw new NotFoundException("Not Found Currency");
        return currency.ToDto<CurrencyDto>();
    }
}