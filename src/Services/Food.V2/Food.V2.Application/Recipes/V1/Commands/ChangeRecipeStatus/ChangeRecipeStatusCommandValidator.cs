namespace Food.V2.Application.Recipes.V1.Commands.ChangeRecipeStatus;

public class ChangeRecipeStatusCommandValidator:AbstractValidator<ChangeRecipeStatusCommand>
{
    public ChangeRecipeStatusCommandValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().NotNull().WithMessage("Id Cannot null or empty");
    }
}