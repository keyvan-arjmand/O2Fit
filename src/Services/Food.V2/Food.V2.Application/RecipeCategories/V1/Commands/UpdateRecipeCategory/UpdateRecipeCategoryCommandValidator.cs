namespace Food.V2.Application.RecipeCategories.V1.Commands.UpdateRecipeCategory;

public class UpdateRecipeCategoryCommandValidator : AbstractValidator<UpdateRecipeCategoryCommand>
{
    public UpdateRecipeCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
        
        RuleFor(x => x.ImageUri).NotEmpty().WithMessage("ImageUri can not be empty")
            .NotNull().WithMessage("ImageUri can not be null");
        
        RuleFor(x => x.TranslationDto.Persian).NotEmpty().WithMessage("Persian can not be empty")
            .NotNull().WithMessage("Persian can not be null");
    }
}