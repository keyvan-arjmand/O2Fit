using Common.Enums.Foods;
using Track.Application.Dtos;

namespace Track.Application.FavoriteMeals.V1.Commands.InsertFavoriteMeals;

public class InsertFavoriteMealsCommand : IRequest<TrackMealDto>
{
    public string UserId { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public decimal CalorieValue { get; set; }
    public FoodMealEvent Meal { get; set; }
    public List<FoodMealDto> TrackFoods { get; set; } = new();
    public string AppId { get; set; } = string.Empty;

}