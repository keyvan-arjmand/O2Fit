namespace Identity.V2.Application.Countries.V1.Queries.GetOldSystemCountryIdByCountryId;

public class GetOldSystemCountryIdByCountryIdQueryHandler: IRequestHandler<GetOldSystemCountryIdByCountryIdQuery,int>
{
    private readonly IUnitOfWork _uow;

    public GetOldSystemCountryIdByCountryIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<int> Handle(GetOldSystemCountryIdByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.CountryRepository()
            .GetOldSystemCountryIdByCountryId(request.CountryId, cancellationToken).ConfigureAwait(false);

        if (result == null)
            throw new NotFoundException(nameof(Country), request.CountryId);

        return (int)result;
    }
}