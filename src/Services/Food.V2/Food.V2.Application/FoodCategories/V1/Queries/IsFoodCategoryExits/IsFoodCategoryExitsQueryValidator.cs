namespace Food.V2.Application.FoodCategories.V1.Queries.IsFoodCategoryExits;

public class IsFoodCategoryExitsQueryValidator : AbstractValidator<IsFoodCategoryExitsQuery>
{
    public IsFoodCategoryExitsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}