namespace Currency.Application.Currencies.V1.Commands.CreateCurrency;

public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    public CreateCurrencyCommandValidator()
    {
        RuleFor(x => x.CoefficientCurrency).GreaterThan(0)
            .NotEmpty().WithMessage("Coefficient Currency can not be Empty")
            .NotNull().WithMessage("Coefficient Currency can not be Null");
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyCode can not be Empty")
            .NotNull().WithMessage("CurrencyCode can not be Null")
            .Must(x => x.Length == 3).WithMessage("CurrencyCode not valid");
        RuleFor(x => x.CountryIds).NotEmpty().WithMessage("CountryIds can not be Empty")
            .NotNull().WithMessage("CountryIds can not be Null")
            .Must(x => x.Count > 0).WithMessage("CountryIds can not empty");
    }
}