namespace Food.V2.Application.Foods.V1.Queries.GetFoodIngredientIds;

public class GetFoodIngredientIdsQueryValidator: AbstractValidator<GetFoodIngredientIdsQuery>
{
    public GetFoodIngredientIdsQueryValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().WithMessage("FoodId can not be empty").NotNull().WithMessage("FoodId can not be null");
    }
}