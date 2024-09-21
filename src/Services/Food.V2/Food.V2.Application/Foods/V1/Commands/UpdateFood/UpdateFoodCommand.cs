namespace Food.V2.Application.Foods.V1.Commands.UpdateFood;

public record UpdateFoodCommand(string Id, CreateFoodTranslationDto Name, string FoodCode, BakingType BakingType,
    TimeSpan BakingTime, string BarcodeGs1 ,string BarcodeNational, List<FoodHabit> FoodHabits,
    List<SpecialDisease> SpecialDiseases, string ImageUri, string ImageThumb, FoodType FoodType,
    string BrandId, List<decimal> Nutrients, List<string> MeasureUnitIds, List<IngredientMeasureUnitDto> Ingredients,
    string Tag, string TagArEn, int PersonCount, bool IsActive, bool UseInDiet, List<string> NationalityIds,
    List<FoodMeal> FoodMeals, List<string> FoodCategoryIds, List<string> DietCategoryIds,
    string DefaultMeasureUnitId, string RecipeCategoryId, List<decimal> CalculatedIngredient, decimal SumWeight, double Gi) : IRequest;