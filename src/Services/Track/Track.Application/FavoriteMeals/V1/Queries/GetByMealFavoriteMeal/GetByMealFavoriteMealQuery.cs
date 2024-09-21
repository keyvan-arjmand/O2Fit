using Common.Enums.Foods;
using Track.Application.Dtos;

namespace Track.Application.FavoriteMeals.V1.Queries.GetByMealFavoriteMeal;

public class GetByMealFavoriteMealQuery : IRequest<List<TrackMealDto>>
{
    public string UserId { get; set; } = string.Empty;
    public FoodMealEvent Meal { get; set; }
}