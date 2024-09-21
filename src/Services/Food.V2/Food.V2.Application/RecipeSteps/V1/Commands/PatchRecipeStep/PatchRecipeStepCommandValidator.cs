namespace Food.V2.Application.RecipeSteps.V1.Commands.PatchRecipeStep;

public class PatchRecipeStepCommandValidator:AbstractValidator<PatchRecipeStepCommand>
{
    public PatchRecipeStepCommandValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().NotNull().WithMessage("FoodId cannot null or empty");
        RuleFor(x => x.Steps).Must(x => x.Count > 0).NotEmpty().NotNull().WithMessage("Steps cannot null or empty");

    }
}