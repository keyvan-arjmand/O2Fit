namespace Food.V2.Application.DietPacks.V1.Commands.UpdateDietPack;

public class UpdateDietPackCommandValidator : AbstractValidator<UpdateDietPackCommand>
{
    public UpdateDietPackCommandValidator()
    {
        RuleFor(x => x.CalorieValue).GreaterThan(0).WithMessage("CalorieValue must greater than zero");
        RuleFor(x => x.DailyCalorie).GreaterThan(0).WithMessage("DailyCalorie must greater than zero");
        RuleFor(x => x.FoodMeal).IsInEnum();
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull()
            .WithMessage("Persian can not be null");
        RuleFor(x=>x.DietCategoryIds).NotEmpty().WithMessage("DietCategoryIds can not be empty").NotNull()
            .WithMessage("DietCategoryId can not be null");
        RuleFor(x=>x.ParentCategory).NotEmpty().WithMessage("ParentCategory can not be empty").NotNull()
            .WithMessage("ParentCategory can not be null");
        RuleForEach(x=>x.NationalityIds).NotEmpty().WithMessage("NationalityIds can not be empty").NotNull()
            .WithMessage("NationalityIds can not be null");
        RuleFor(x => x.DietPackFoods).Must(x => x.Count > 0).WithMessage("DietPackFoods can not be empty");
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull()
            .WithMessage("Id can not be null");
    }
}