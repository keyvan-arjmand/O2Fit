namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExits;

public record IsDietPackExitsQuery(int CalorieValue, string Persian, int DailyCalorie, FoodMeal FoodMeal, string DietCategoryId, List<decimal> NutritionValue) : IRequest<bool>;