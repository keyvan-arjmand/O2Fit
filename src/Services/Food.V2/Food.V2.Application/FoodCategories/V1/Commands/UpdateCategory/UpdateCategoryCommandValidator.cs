namespace Food.V2.Application.FoodCategories.V1.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Translation).NotNull().WithMessage("Translation Cannot be null");
        RuleFor(x => x.Id).NotNull().WithMessage("Id Cannot be null")
            .NotEmpty().WithMessage("Id Cannot be Empty");
    }
}