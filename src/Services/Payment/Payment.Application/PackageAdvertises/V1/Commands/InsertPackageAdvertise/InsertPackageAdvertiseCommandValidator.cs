namespace Payment.Application.PackageAdvertises.V1.Commands.InsertPackageAdvertise;

public class InsertPackageAdvertiseCommandValidator : AbstractValidator<InsertPackageAdvertiseCommand>
{
    public InsertPackageAdvertiseCommandValidator()
    {
        RuleFor(x => x).Must(x => x.Price.ModNumber(x.Cost) == 0)
            .WithMessage("The value of Price is not divisible by Cost");
        RuleFor(x=>x.Cost).NotEmpty().NotNull().WithMessage("Cost can not be Null or empty");
        RuleFor(x=>x.Price).NotEmpty().NotNull().WithMessage("Cost can not be Null or empty");
        RuleFor(x=>x.CurrencyCode).NotEmpty().NotNull().WithMessage("Cost can not be Null or empty");
        RuleFor(x=>x.AdvertisePackageType).IsInEnum();
        RuleFor(x=>x.IsActive).NotEmpty().NotNull().WithMessage("Cost can not be Null or empty");
        RuleFor(x=>x.TranslationTitle).NotEmpty().NotNull().WithMessage("Cost can not be Null or empty");
    }
}