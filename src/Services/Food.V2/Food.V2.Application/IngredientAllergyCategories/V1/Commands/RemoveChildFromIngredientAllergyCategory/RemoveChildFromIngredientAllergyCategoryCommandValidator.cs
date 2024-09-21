namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.RemoveChildFromIngredientAllergyCategory;

public class RemoveChildFromIngredientAllergyCategoryCommandValidator : AbstractValidator<RemoveChildFromIngredientAllergyCategoryCommand>
{
    public RemoveChildFromIngredientAllergyCategoryCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("RootId can not be empty")
            .NotNull().WithMessage("RootId can not be null");
        
        RuleFor(x=>x.ChildId).NotEmpty().WithMessage("ChildId can not be empty")
            .NotNull().WithMessage("ChildId can not be null");
    }
}