namespace Identity.V2.Application.Countries.V1.Commands.UpdateUnCountryInfo;

public class UpdateUnCountryInfoCommandValidator:AbstractValidator<UpdateUnCountryInfoCommand>
{
    public UpdateUnCountryInfoCommandValidator()
    {
        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("CountryCode can not be empty")
            .NotNull().WithMessage("CountryCode can not be null");
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
        
        RuleFor(x => x.Alpha)
            .NotEmpty().WithMessage("Alpha can not be empty")
            .NotNull().WithMessage("Alpha can not be null")
            .MaximumLength(3).WithMessage("Alpha maxlength is 3 character");
        
        RuleFor(x => x.Culture)
            .NotEmpty().WithMessage("Culture can not be empty")
            .NotNull().WithMessage("Culture can not be null")
            .MaximumLength(10).WithMessage("Culture maxlength is 10 character");
        
        RuleFor(x => x.CurrencyCode)
            .NotEmpty().WithMessage("CurrencyCode can not be empty")
            .NotNull().WithMessage("CurrencyCode can not be null")
            .MaximumLength(3).WithMessage("CurrencyCode maxlength is 3 character");

        
        RuleFor(x => x.CurrencyName)
            .NotEmpty().WithMessage("CurrencyName can not be empty")
            .NotNull().WithMessage("CurrencyName can not be null")
            .MaximumLength(50).WithMessage("CurrencyName maxlength is 50 character");
        
        RuleFor(x => x.UtcTime)
            .NotEmpty().WithMessage("UtcTime can not be empty")
            .NotNull().WithMessage("UtcTime can not be null")
            .MaximumLength(7).WithMessage("UtcTime maxlength is 7 character");
    }
}