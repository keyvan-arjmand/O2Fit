namespace Identity.V2.Application.Countries.V1.Queries.GetCountryIdByOldSystemCountryId;

public class GetCountryIdByOldSystemCountryIdQueryHandler : IRequestHandler<GetCountryIdByOldSystemCountryIdQuery, string>
{
    private readonly IUnitOfWork _uow;

    public GetCountryIdByOldSystemCountryIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(GetCountryIdByOldSystemCountryIdQuery request, CancellationToken cancellationToken)
    {
        var countryId = await _uow.CountryRepository()
            .GetCountryIdByOldSystemCountryIdAsync(request.OldSystemCountryId, cancellationToken);
        if(string.IsNullOrEmpty(countryId))
            throw new NotFoundException(nameof(Country), request.OldSystemCountryId);

        return countryId;
    }
}