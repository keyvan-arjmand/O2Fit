namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.CreateRootIngredientAllergy;

public class CreateRootIngredientAllergyCommandValidator : AbstractValidator<CreateRootIngredientAllergyCommand>
{
    public CreateRootIngredientAllergyCommandValidator()
    {
        RuleFor(x => x.RootId).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}