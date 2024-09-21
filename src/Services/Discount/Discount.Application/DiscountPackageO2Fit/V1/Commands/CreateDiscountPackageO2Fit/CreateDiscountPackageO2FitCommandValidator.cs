namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.CreateDiscountPackageO2Fit;

public class CreateDiscountPackageO2FitCommandValidator:AbstractValidator<CreateDiscountPackageO2FitCommand>
{
    public CreateDiscountPackageO2FitCommandValidator()
    {
        RuleFor(x => x.Percent).NotEmpty().WithMessage("Percent can not be empty").NotNull().WithMessage("Percent can not be Null")
            .GreaterThan(0).WithMessage("Percent Not Valid").LessThanOrEqualTo(100).WithMessage("Percent Not Valid");
        RuleFor(x => x.PackageId).NotEmpty().WithMessage("PackageId can not be empty").NotNull().WithMessage("PackageId can not be Null");
    }
}