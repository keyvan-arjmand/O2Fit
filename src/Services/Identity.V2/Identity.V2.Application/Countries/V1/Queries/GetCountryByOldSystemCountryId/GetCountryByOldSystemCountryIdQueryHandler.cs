namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByOldSystemCountryId;

public class GetCountryByOldSystemCountryIdQueryHandler : IRequestHandler<GetCountryByOldSystemCountryIdQuery, CountryDto>
{
    private readonly IUnitOfWork _uow;

    public GetCountryByOldSystemCountryIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CountryDto> Handle(GetCountryByOldSystemCountryIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Country>.Filter.Eq(x => x.CountryId, request.CountryId);
        var country = await _uow.GenericRepository<Country>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.CountryId);

        return country.ToDto<CountryDto>();
    }
}