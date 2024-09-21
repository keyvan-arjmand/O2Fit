namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.CreateDiscountPackageNutritionist;

public class
    CreateDiscountPackageNutritionistCommandValidator : AbstractValidator<CreateDiscountPackageNutritionistCommand>
{
    public CreateDiscountPackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Percent).NotEmpty().WithMessage("Percent can not be empty").NotNull()
            .WithMessage("Percent can not be Null")
            .GreaterThan(0).WithMessage("Percent Not Valid").LessThanOrEqualTo(100).WithMessage("Percent Not Valid");
        RuleFor(x => x.PackageId).NotEmpty().WithMessage("PackageId can not be empty").NotNull()
            .WithMessage("PackageId can not be Null");
    }
}