namespace Food.V2.Application.RecipeSteps.V1.Commands.CreateRecipeSteps;

public class CreateRecipeStepsCommandValidator : AbstractValidator<CreateRecipeStepsCommand>
{
    public CreateRecipeStepsCommandValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().NotNull().WithMessage("FoodId cannot null or empty");
        RuleFor(x => x.Steps).NotEmpty().NotNull().Must(x => x.Count > 0).WithMessage("Steps cannot null or empty");
    }
}