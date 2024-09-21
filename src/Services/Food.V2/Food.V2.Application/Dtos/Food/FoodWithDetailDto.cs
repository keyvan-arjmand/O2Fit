using Food.V2.Application.Dtos.Brand;
using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.Dtos.Food;

[BsonIgnoreExtraElements]
public class FoodWithDetailDto
{
    public string Id { get; set; } = string.Empty;
    public FoodTranslation Name { get; set; } = null!;
    public string FoodCode { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string BarcodeGs1 { get; set; } = string.Empty;
    public string BarcodeNational { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public string ImageThumb { get; set; } = string.Empty;
    public decimal WeightBeforeBaking { get; set; }
    public decimal WeightAfterBaking { get; set; }
    public decimal EvaporatedWater { get; set; }
    public decimal DryIngredient { get; set; }
    public FoodType FoodType { get; set; }
    public bool IsActive { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string TagArEn { get; set; } = string.Empty;
    public List<decimal> NutrientValue { get; set; } = null!;
    public int PersonCount { get; set; }
    public List<FoodMeal> FoodMeals { get; set; } = null!;
    public List<FoodHabit> FoodHabits { get; set; } = null!;
    public List<SpecialDisease> SpecialDiseases { get; set; } = null!;
    public decimal? Gl { get; set; }
    public decimal? Gi { get; set; }
    public bool UseInDiet { get; set; }
    public string BrandId { get; set; } = string.Empty;
    public List<BrandDto> Brand { get; set; } = null!;
    public List<MeasureUnitDto> DefaultMeasureUnit { get; set; } = null!;
    public RecipeStatus RecipeStatus { get; set; }
    public List<FoodTranslation> RecipeSteps { get; set; } = new();
    public List<FoodTranslation> RecipeTips { get; set; } = new();
    public List<RecipeCategoryDto> RecipeCategories { get; set; } = null!;

    public List<NationalityDto> Nationalities { get; set; } = null!;

    public List<FoodIngredientDto> FoodIngredients { get; set; } = null!;
    public List<MeasureUnitDto> MeasureUnits { get; set; } = null!;
    public List<FoodCategoryDto> FoodCategories { get; set; } = null!;

    public List<DietCategoryDto> DietCategories { get; set; } = null!;

    public string Lang { get; set; } = string.Empty;
    // public ObjectId DefaultMeasureUnitId { get; set; }
    // public ObjectId RecipeCategoryId { get; set; }
    // public List<ObjectId> IngredientIds { get; set; } = null!;
    // public List<ObjectId> MeasureUnitIds { get; set; } = null!;
    // public List<ObjectId> NationalityIds { get; set; } = null!;
    // public List<ObjectId> FoodCategoryIds { get; set; } = null!;
    // public List<ObjectId> DietCategoryIds { get; set; } = null!;
}