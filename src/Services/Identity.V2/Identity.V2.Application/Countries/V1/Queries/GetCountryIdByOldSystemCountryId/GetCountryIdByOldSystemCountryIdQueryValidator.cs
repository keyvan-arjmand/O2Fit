namespace Identity.V2.Application.Countries.V1.Queries.GetCountryIdByOldSystemCountryId;

public class GetCountryIdByOldSystemCountryIdQueryValidator: AbstractValidator<GetCountryIdByOldSystemCountryIdQuery>
{
    public GetCountryIdByOldSystemCountryIdQueryValidator()
    {
        RuleFor(x => x.OldSystemCountryId)
            .GreaterThan(0).WithMessage("Old system country id can not be less than 1");
    }
}