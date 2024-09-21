namespace Food.V2.Application.DietCategories.V1.Queries.IsDietCategoryExits;

public class IsDietCategoryExitsQueryValidator : AbstractValidator<IsDietCategoryExitsQuery>
{
    public IsDietCategoryExitsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}