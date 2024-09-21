namespace Food.V2.Application.RecipeSteps.V1.Queries.GetRecipeStepsByFoodId;

public class GetRecipeStepsByFoodIdQueryValidator : AbstractValidator<GetRecipeStepsByFoodIdQuery>
{
    public GetRecipeStepsByFoodIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("id cannot null or empty");
    }
}