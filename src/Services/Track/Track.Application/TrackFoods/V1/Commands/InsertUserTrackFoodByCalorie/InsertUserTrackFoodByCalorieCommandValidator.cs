namespace Track.Application.TrackFoods.V1.Commands.InsertUserTrackFoodByCalorie;

public class InsertUserTrackFoodByCalorieCommandValidator : AbstractValidator<InsertUserTrackFoodByCalorieCommand>
{
    public InsertUserTrackFoodByCalorieCommandValidator()
    {
        RuleFor(x => x.InsertDate).NotEmpty().NotNull().WithMessage("DateTime can not empty or null");
        RuleFor(x => x.FoodNutrientValue)
            .Must(x => x.Count == 34)
            .WithMessage("FoodNutrientValue Must 34 item")
            .NotEmpty().NotNull().WithMessage("FoodNutrientValue can not empty or null");
        RuleFor(x => x.FoodMeal).IsInEnum();
        RuleFor(x => x.AppId).NotEmpty().NotNull().WithMessage("AppId can not empty or null");
    }
}