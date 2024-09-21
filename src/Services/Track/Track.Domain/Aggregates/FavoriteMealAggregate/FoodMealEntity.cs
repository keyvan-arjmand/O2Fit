using Common.Enums.Foods;
using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.FavoriteMealAggregate;

public class FoodMealEntity : BaseEntity
{
    public FoodMealEntity()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public NotNegativeForDecimalTypes Value { get; set; } = new();
    public FoodMealEvent FoodMeal { get; set; }
    public List<NotNegativeForDecimalTypes> NutrientValue { get; set; } = new();
    public ObjectId MeasureUnitId { get; set; }
    public string MeasureUnitName { get; set; } = string.Empty;
    public ObjectId? PersonalFoodId { get; set; }
    public ObjectId? FoodId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
}