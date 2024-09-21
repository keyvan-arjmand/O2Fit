namespace Food.V2.Application.Foods.V1.Commands.CreateFood;

public record CreateFoodCommand(List<decimal> CalculatedIngredient, decimal SumWeight, string FoodCode, BakingType BakingType, TimeSpan BakingTime, string BarcodeGs1,
    string BarcodeNational, string ImageUri, string ImageThumb, FoodType FoodType, bool IsActive,  string Tag, string TagArEn, List<decimal> NutrientValue,  string BrandId,
    int PersonCount, List<FoodMeal> FoodMeals, double Gi, bool UseInDiet, List<FoodHabit> FoodHabits, string DefaultMeasureUnitId, string RecipeCategoryId,
    List<string> NationalityIds, List<string> FoodCategoryIds, List<string> DietCategoryIds, List<string> MeasureUnitIds, List<IngredientMeasureUnitDto> Ingredients,
    CreateFoodTranslationDto Name, List<SpecialDisease> SpecialDiseases) : IRequest;