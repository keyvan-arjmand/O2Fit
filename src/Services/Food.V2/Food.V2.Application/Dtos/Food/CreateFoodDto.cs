namespace Food.V2.Application.Dtos.Food;

public class CreateFoodDto
{
    public CreateFoodTranslationDto Name { get; set; } = null!;

    public string FoodCode { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string BarcodeGs1 { get; set; } = string.Empty;
    public string BarcodeNational { get; set; } = string.Empty;
    public List<FoodHabit> FoodHabits { get; set; } = null!;
    public List<SpecialDisease> SpecialDiseases { get; set; } = null!;
    public string ImageUri { get; set; } = string.Empty;
    public string ImageThumb { get; set; } = string.Empty;
    public FoodType FoodType { get; set; }

    public string BrandId { get; set; } = string.Empty;
    public List<string> MeasureUnitIds { get; set; } = null!;
    public List<IngredientMeasureUnitDto>? Ingredients { get; set; }
    public List<decimal> Nutrients { get; set; } = null!;
    public string Tag { get; set; } = string.Empty;
    public string TagArEn { get; set; } = string.Empty;
    public int PersonCount { get; set; }
    public bool IsActive { get; set; }
    public double Gi { get; set; }
    public bool UseInDiet { get; set; }
    public List<string> NationalityIds { get; set; } = null!;

    public List<FoodMeal> FoodMeals { get; set; } = null!;

    public List<string> FoodCategoryIds { get; set; } = null!;

    public List<string> DietCategoryIds { get; set; } = null!;
    public string DefaultMeasureUnitId { get; set; } = string.Empty;
    public string RecipeCategoryId { get; set; } = string.Empty;
}