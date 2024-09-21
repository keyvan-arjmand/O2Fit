namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByCurrencyCode;

public class GetCountryByCurrencyCodeQueryValidator: AbstractValidator<GetCountryByCurrencyCodeQuery>
{
    public GetCountryByCurrencyCodeQueryValidator()
    {
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyCode can not be empty")
            .NotNull().WithMessage("CurrencyCode can not be null");
    }
}