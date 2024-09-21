namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByOldSystemCountryId;

public class GetCountryByOldSystemCountryIdQueryValidator : AbstractValidator<GetCountryByOldSystemCountryIdQuery>
{
    private readonly IUnitOfWork _uow;

    public GetCountryByOldSystemCountryIdQueryValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.CountryId).GreaterThan(0)
            .WithMessage("CountryId of old system should be greater than zero");
        //.MustAsync(IsExists).WithMessage("CountryId of old system is not exists");
    }

    // private async Task<bool> IsExists(int countryId, CancellationToken cancellationToken)
    // {
    //     var filter = Builders<Country>.Filter.Eq(x => x.CountryId, countryId);
    //     var country = await _uow.GenericRepository<Country>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
    //     return country != null;
    // }
}