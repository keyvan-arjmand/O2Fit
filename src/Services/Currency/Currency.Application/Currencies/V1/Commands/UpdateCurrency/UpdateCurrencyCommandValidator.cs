namespace Currency.Application.Currencies.V1.Commands.UpdateCurrency;

public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
{
    public UpdateCurrencyCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
        RuleFor(x => x.CoefficientCurrency).GreaterThan(0)
            .NotEmpty().WithMessage("Coefficient Currency can not be Empty")
            .NotNull().WithMessage("Coefficient Currency can not be Null");
        RuleFor(x => x.CountryIds).NotEmpty().WithMessage("CountryIds can not be Empty")
            .NotNull().WithMessage("CountryIds can not be Null")
            .Must(x => x.Count > 0).WithMessage("CountryIds can not empty");
    }
}