namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByCurrencyCode;

public class GetCountryByCurrencyCodeQueryHandler : IRequestHandler<GetCountryByCurrencyCodeQuery, CountryDto>
{
    private readonly IUnitOfWork _uow;

    public GetCountryByCurrencyCodeQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CountryDto> Handle(GetCountryByCurrencyCodeQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Country>.Filter.Eq(x => x.CurrencyCode, request.CurrencyCode);
        var country = await _uow.GenericRepository<Country>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.CurrencyCode);

        return country.ToDto<CountryDto>();
    }
}