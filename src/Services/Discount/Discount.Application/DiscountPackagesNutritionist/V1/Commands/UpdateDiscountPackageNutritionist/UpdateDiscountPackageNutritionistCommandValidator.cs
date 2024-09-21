namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.UpdateDiscountPackageNutritionist;

public class
    UpdateDiscountPackageNutritionistCommandValidator : AbstractValidator<UpdateDiscountPackageNutritionistCommand>
{
    public UpdateDiscountPackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Percent).NotEmpty().WithMessage("Percent can not be empty").NotNull()
            .WithMessage("Percent can not be Null")
            .GreaterThan(0).WithMessage("Percent Not Valid").LessThanOrEqualTo(100).WithMessage("Percent Not Valid");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be Null");
    }
}