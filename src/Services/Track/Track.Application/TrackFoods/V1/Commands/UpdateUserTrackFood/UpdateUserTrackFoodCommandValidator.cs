namespace Track.Application.TrackFoods.V1.Commands.UpdateUserTrackFood;

public class UpdateUserTrackFoodCommandValidator : AbstractValidator<UpdateUserTrackFoodCommand>
{
    public UpdateUserTrackFoodCommandValidator()
    {
        RuleFor(x => x.InsertDate).NotEmpty().NotNull().WithMessage("DateTime can not empty or null");
        RuleFor(x => x).Must(x => CheckIds(x.FoodId, x.PersonalFoodId));
        RuleFor(x => x.FoodNutrientValue)
            .Must(x => x.Count == 34)
            .WithMessage("FoodNutrientValue Must 34 item")
            .NotEmpty().NotNull().WithMessage("FoodNutrientValue can not empty or null");
        RuleFor(x => x.FoodMeal).IsInEnum();
        RuleFor(x => x.AppId).NotEmpty().NotNull().WithMessage("AppId can not empty or null");
        RuleFor(x => x.MeasureUnitId).NotEmpty().NotNull().WithMessage("MeasureUnitId can not empty or null");
        RuleFor(x => x.Value).GreaterThan(0).NotEmpty().NotNull().WithMessage("DateTime can not empty or null");
    }

    private bool CheckIds(string foodId, string personalFoodId)
    {
        switch (string.IsNullOrEmpty(foodId), string.IsNullOrEmpty(personalFoodId))
        {
            case (true, true):
                return false;
            case (false, false):
                return false;
            case (true, false):
                return true;
            case (false, true):
                return true;
        }
    }
}