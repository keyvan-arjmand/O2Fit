using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Domain.Aggregates.FoodAggregate;

public class Food : AggregateRoot
{
    public string FoodCode { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string? BarcodeGs1 { get; set; }
    public string? BarcodeNational { get; set; }
    public string? ImageUri { get; set; }
    public string? ImageThumb { get; set; }
    public NotNegativeForDecimalTypes WeightBeforeBaking { get; set; }
    public NotNegativeForDecimalTypes WeightAfterBaking { get; set; }

    //[BsonRepresentation(BsonType.Decimal128)]
    public decimal EvaporatedWater { get; set; }

    //[BsonRepresentation(BsonType.Decimal128)]
    public decimal DryIngredient { get; set; }
    public FoodType FoodType { get; set; }
    public bool IsActive { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string TagArEn { get; set; } = string.Empty;
    public List<NotNegativeForDecimalTypes> NutrientValue { get; set; } = new();
    public int PersonCount { get; set; }
    public List<FoodMeal> FoodMeals { get; set; } = new();
    public List<FoodHabit> FoodHabits { get; set; } = new();
    public List<SpecialDisease> SpecialDiseases { get; set; } = new();

    public NotNegativeForDoubleTypes Gl { get; set; }

    public NotNegativeForDoubleTypes Gi { get; set; }

    // public NotNegativeForDoubleTypes? IngredientValue { get; set; }
    public bool UseInDiet { get; set; }

    public FoodTranslation Name { get; set; } = null!;
    public RecipeStatus RecipeStatus { get; set; }
    public List<FoodTranslation> RecipeSteps { get; set; } = new();
    public List<FoodTranslation> RecipeTips { get; set; } = new();
    public ObjectId BrandId { get; set; }
    public ObjectId DefaultMeasureUnitId { get; set; }
    public ObjectId RecipeCategoryId { get; set; }

    public List<FoodIngredient> FoodIngredients { get; set; } = new();
    public List<ObjectId> MeasureUnitIds { get; set; } = new();
    public List<ObjectId> NationalityIds { get; set; } = new();
    public List<ObjectId> FoodCategoryIds { get; set; } = new();
    public List<ObjectId> DietCategoryIds { get; set; } = new();
}