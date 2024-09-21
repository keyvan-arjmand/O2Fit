namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddChildrenToIngredientAllergyCategory;

public class AddChildrenToIngredientAllergyCategoryCommandValidator : AbstractValidator<AddChildrenToIngredientAllergyCategoryCommand>
{
    public AddChildrenToIngredientAllergyCategoryCommandValidator()
    {
        RuleFor(x=>x.RootId).NotEmpty().WithMessage("RootId can not be empty")
            .NotNull().WithMessage("RootId can not be null");
        
        RuleFor(x=>x.ChildId).NotEmpty().WithMessage("ChildId can not be empty")
            .NotNull().WithMessage("ChildId can not be null");
    }
}