namespace Food.V2.Application.RecipeCategories.V1.Commands.SoftDeleteRecipeCategory;

public class SoftDeleteRecipeCategoryCommandValidator : AbstractValidator<SoftDeleteRecipeCategoryCommand>
{
    public SoftDeleteRecipeCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");

    }
}