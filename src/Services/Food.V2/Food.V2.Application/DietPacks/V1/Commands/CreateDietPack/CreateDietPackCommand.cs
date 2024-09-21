namespace Food.V2.Application.DietPacks.V1.Commands.CreateDietPack;

public record CreateDietPackCommand(CreateDietPackTranslationDto Name, FoodMeal FoodMeal, List<SpecialDisease> SpecialDiseases, int CalorieValue, List<decimal> NutrientValues,
                                    bool IsActive, List<string> DietCategoryIds, int DailyCalorie,  string ParentCategory, List<string> NationalityIds, 
                                    List<CreateDietPackFoodDto> DietPackFoods, List<string> IngredientAllergies) : IRequest<string>;
