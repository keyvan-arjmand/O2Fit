namespace Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;

public class GetByIdRecipeQueryValidator:AbstractValidator<GetByIdRecipeQuery>
{
    public GetByIdRecipeQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("id cannot null or empty");
    }
}