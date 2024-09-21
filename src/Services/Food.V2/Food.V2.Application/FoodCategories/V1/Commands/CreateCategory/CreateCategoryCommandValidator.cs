namespace Food.V2.Application.FoodCategories.V1.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Translation).NotNull().WithMessage("Translation can not be null");
    }
}