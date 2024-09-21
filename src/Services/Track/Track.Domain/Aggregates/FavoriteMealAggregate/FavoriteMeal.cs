using Common.Enums.Foods;
using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.FavoriteMealAggregate;

public class FavoriteMeal : AggregateRoot
{
    public int UserId { get; set; }
    public NotNegativeForDecimalTypes CalorieValue { get; set; } = new();
    public FoodMealEvent Meal { get; set; }
    public List<FoodMealEntity> Foods { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
}