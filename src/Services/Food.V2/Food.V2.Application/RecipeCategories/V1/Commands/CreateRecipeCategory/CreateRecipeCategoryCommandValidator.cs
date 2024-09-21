namespace Food.V2.Application.RecipeCategories.V1.Commands.CreateRecipeCategory;

public class CreateRecipeCategoryCommandValidator : AbstractValidator<CreateRecipeCategoryCommand>
{
    public CreateRecipeCategoryCommandValidator()
    {
        RuleFor(x => x.ImageUri).NotEmpty().WithMessage("ImageUri can not be empty")
            .NotNull().WithMessage("ImageUri can not be null");
        
        RuleFor(x => x.TranslationDto.Persian).NotEmpty().WithMessage("Persian can not be empty")
            .NotNull().WithMessage("Persian can not be null");
    }
}