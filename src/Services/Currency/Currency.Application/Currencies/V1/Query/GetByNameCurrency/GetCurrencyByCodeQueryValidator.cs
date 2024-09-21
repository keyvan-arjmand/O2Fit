namespace Currency.Application.Currencies.V1.Query.GetByNameCurrency;

public class GetCurrencyByCodeQueryValidator : AbstractValidator<GetCurrencyByCodeQuery>
{
    public GetCurrencyByCodeQueryValidator()
    {
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyCode can not be Empty")
            .NotNull().WithMessage("CurrencyCode can not be Null")
            .Must(x => x.Length == 3).WithMessage("CurrencyCode not valid");
    }
}