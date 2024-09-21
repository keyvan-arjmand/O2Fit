namespace Food.V2.Application.Foods.V1.Commands.UpdateFood;

public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
{
    public UpdateFoodCommandValidator()
    {
        RuleFor(x => x.Nutrients).NotEmpty().WithMessage("Nutrients can not be empty")
            .NotNull().WithMessage("Nutrients can not be null");
        RuleFor(x => x.MeasureUnitIds).NotEmpty().WithMessage("MeasureUnitIds can not be empty")
            .NotNull().WithMessage("MeasureUnitIds can not be null");
        RuleFor(x => x.DefaultMeasureUnitId).NotEmpty().WithMessage("DefaultMeasureUnitId can not be empty")
            .NotNull().WithMessage("DefaultMeasureUnitId can not be null");
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Name.Persian can not be empty")
            .NotNull().WithMessage("Name.Persian can not be null");
        RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag can not be empty").NotNull().WithMessage("Tag can not be null");
        RuleFor(x => x.TagArEn).NotEmpty().WithMessage("TagArEn can not be empty").NotNull().WithMessage("TagArEn can not be null");
        RuleFor(x => x.FoodCode).NotEmpty().WithMessage("FoodCode can not be empty").NotNull().WithMessage("FoodCode can not be null");
        RuleFor(x => x.FoodType).IsInEnum();
        RuleFor(x => x.FoodCategoryIds).NotEmpty().WithMessage("FoodCategoryIds can not be empty")
            .NotNull().WithMessage("FoodCategoryIds can not be null");
        RuleFor(x => x.DietCategoryIds).NotEmpty().WithMessage("FoodCategoryIds can not be empty").NotNull().WithMessage("FoodCategoryIds can not be null");
        RuleFor(x => x.NationalityIds).NotEmpty().WithMessage("NationalityIds can not be empty").NotNull().WithMessage("NationalityIds can not be null");
        RuleFor(x => x.FoodMeals).NotEmpty().WithMessage("FoodMeals can not be empty").NotNull().WithMessage("FoodMeals can not be null");
        RuleFor(x => x.FoodHabits).NotEmpty().WithMessage("FoodHabits can not be empty").NotNull().WithMessage("FoodHabits can not be null");

    }
}