using Common.Enums.Foods;
using Track.Application.Dtos;

namespace Track.Application.FavoriteMeals.V1.Commands.PartialUpdateFavoriteMeals;

public record PatchFavoriteMealFoodsCommand
    (string UserId, FoodMealEvent Meal, List<FoodMealDto> Foods, decimal CalorieValue, string ImageUri) : IRequest;