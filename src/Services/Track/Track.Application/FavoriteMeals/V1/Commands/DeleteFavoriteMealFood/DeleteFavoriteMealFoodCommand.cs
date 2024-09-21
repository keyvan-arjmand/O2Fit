using Common.Enums.Foods;

namespace Track.Application.FavoriteMeals.V1.Commands.DeleteFavoriteMealFood;

public record DeleteFavoriteMealFoodCommand(string FoodId, FoodMealEvent Meal, string UserId) : IRequest;