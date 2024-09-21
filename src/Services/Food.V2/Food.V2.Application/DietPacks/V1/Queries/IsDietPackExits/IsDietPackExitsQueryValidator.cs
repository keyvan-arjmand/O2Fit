namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExits;

public class IsDietPackExitsQueryValidator : AbstractValidator<IsDietPackExitsQuery>
{
    public IsDietPackExitsQueryValidator()
    {
        RuleFor(x => x.CalorieValue).GreaterThan(0).WithMessage("CalorieValue must greater than zero");
        RuleFor(x => x.DailyCalorie).GreaterThan(0).WithMessage("DailyCalorie must greater than zero");
        RuleFor(x => x.FoodMeal).IsInEnum();
        RuleFor(x => x.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull()
            .WithMessage("Persian can not be null");
        RuleFor(x=>x.DietCategoryId).NotEmpty().WithMessage("DietCategoryId can not be empty").NotNull()
            .WithMessage("DietCategoryId can not be null");

    }
}