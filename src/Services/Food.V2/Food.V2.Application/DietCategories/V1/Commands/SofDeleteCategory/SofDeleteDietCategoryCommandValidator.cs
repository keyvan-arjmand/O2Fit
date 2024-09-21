using Food.V2.Application.Nationalities.V1.Commands.SoftDeleteNationality;

namespace Food.V2.Application.DietCategories.V1.Commands.SofDeleteCategory;

public class SofDeleteDietCategoryCommandValidator : AbstractValidator<SoftDeleteNationalityCommand>
{
    public SofDeleteDietCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id Cannot be null")
            .NotEmpty().WithMessage("Id Cannot be Empty");
    }
}