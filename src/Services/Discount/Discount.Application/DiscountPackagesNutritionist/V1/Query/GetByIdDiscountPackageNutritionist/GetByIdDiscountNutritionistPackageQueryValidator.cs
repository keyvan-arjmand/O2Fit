namespace Discount.Application.DiscountPackagesNutritionist.V1.Query.GetByIdDiscountPackageNutritionist;

public class GetByIdDiscountNutritionistPackageQueryValidator:AbstractValidator<GetByIdDiscountNutritionistPackageQuery>
{
    public GetByIdDiscountNutritionistPackageQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can not be empty").NotNull().WithMessage("Id Can not be null");
    }
}