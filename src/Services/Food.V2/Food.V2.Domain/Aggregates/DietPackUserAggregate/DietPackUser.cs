using Food.V2.Domain.Aggregates.DietPackAggregate;

namespace Food.V2.Domain.Aggregates.DietPackUserAggregate;

public class DietPackUser: BaseEntity
{
    public DietPackUserTranslation Translation { get; set; } = null!;
    public FoodMeal FoodMeal { get; set; }
    public List<SpecialDisease> SpecialDiseases { get; set; } = new();
    public NonNegativeForIntegerTypes CalorieValue { get; set; } = null!;
    public List<NotNegativeForDecimalTypes> NutrientValues { get; set; } = new();
    public bool IsActive { get; set; }
    public ObjectId ParentCategoryId { get; set; }
    public List<ObjectId> DietCategoryIds { get; set; } = new();
    public NonNegativeForIntegerTypes DailyCalorie { get; set; } = null!;
    public List<DietPackFood> DietPackFoods { get; set; } = new();
    public List<ObjectId> IngredientAllergy { get; set; } = new();
    public List<ObjectId> NationalityIds { get; set; } = new();
    public int Repeat { get; set; }
}