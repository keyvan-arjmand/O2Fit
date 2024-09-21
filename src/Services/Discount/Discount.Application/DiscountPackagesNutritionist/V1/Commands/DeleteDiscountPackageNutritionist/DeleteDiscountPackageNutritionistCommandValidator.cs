namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.DeleteDiscountPackageNutritionist;

public class DeleteDiscountPackageNutritionistCommandValidator : AbstractValidator<DeleteDiscountPackageNutritionistCommand>
{

    public DeleteDiscountPackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
