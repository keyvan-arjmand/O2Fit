namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddIngredientAllergyToRoot;

public class AddIngredientAllergyToRootCommandValidator : AbstractValidator<AddIngredientAllergyToRootCommand>
{

    public AddIngredientAllergyToRootCommandValidator()
    {
        RuleFor(x => x.IngredientAllergyId).NotEmpty().WithMessage("IngredientAllergyId can not be empty")
            .NotNull().WithMessage("IngredientAllergyId can not be null");
        
        RuleFor(x => x.RootAllergyId).NotEmpty().WithMessage("RootAllergyId can not be empty")
            .NotNull().WithMessage("RootAllergyId can not be null");
    }
}