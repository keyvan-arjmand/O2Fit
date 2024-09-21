namespace Food.V2.Application.DietCategories.V1.Commands.UpdateDietCategory;

public class UpdateDietCategoryCommandValidator:AbstractValidator<UpdateDietCategoryCommand>
{
    public UpdateDietCategoryCommandValidator()
    {
        RuleFor(x => x.Description).NotNull().WithMessage("Translation Cannot be null");

        RuleFor(x => x.Name).NotNull().WithMessage("Translation Cannot be null");

        RuleFor(x => x.Id).NotNull().WithMessage("Id Cannot be null")
            .NotEmpty().WithMessage("Id Cannot be Empty");
    }
}