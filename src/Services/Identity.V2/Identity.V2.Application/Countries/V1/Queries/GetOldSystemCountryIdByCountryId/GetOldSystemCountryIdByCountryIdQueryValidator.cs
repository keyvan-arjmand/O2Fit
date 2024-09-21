namespace Identity.V2.Application.Countries.V1.Queries.GetOldSystemCountryIdByCountryId;

public class GetOldSystemCountryIdByCountryIdQueryValidator: AbstractValidator<GetOldSystemCountryIdByCountryIdQuery>
{
    public GetOldSystemCountryIdByCountryIdQueryValidator()
    {
        RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId can not be empty")
            .NotNull().WithMessage("CountryId can not be null");
    }
}