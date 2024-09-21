namespace Food.V2.Application.DietCategories.V1.Commands.CreateDietCategory;

public class CreateDietCategoryCommandValidator:AbstractValidator<CreateDietCategoryCommand>
{
    public CreateDietCategoryCommandValidator()
    {
        RuleFor(x => x.Description).NotNull().WithMessage("Translation Cannot be null");

        RuleFor(x => x.Name).NotNull().WithMessage("Translation Cannot be null");

    }
}