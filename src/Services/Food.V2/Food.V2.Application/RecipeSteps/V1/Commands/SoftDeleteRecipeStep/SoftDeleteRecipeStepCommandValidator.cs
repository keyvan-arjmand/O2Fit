namespace Food.V2.Application.RecipeSteps.V1.Commands.SoftDeleteRecipeStep;

public class SoftDeleteRecipeStepCommandValidator:AbstractValidator<SoftDeleteRecipeStepCommand>
{
    public SoftDeleteRecipeStepCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id cannot null or empty");

    }
}