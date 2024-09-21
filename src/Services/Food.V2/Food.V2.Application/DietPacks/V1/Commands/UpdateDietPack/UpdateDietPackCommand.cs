namespace Food.V2.Application.DietPacks.V1.Commands.UpdateDietPack;

public record UpdateDietPackCommand(string Id, CreateDietPackTranslationDto Name, FoodMeal FoodMeal,
    List<SpecialDisease> SpecialDiseases, int CalorieValue, List<decimal> NutrientValue,
    bool IsActive, List<string> DietCategoryIds, int DailyCalorie, string ParentCategory, List<string> NationalityIds,
    List<CreateDietPackFoodDto> DietPackFoods, List<string> IngredientAllergies) : IRequest;
