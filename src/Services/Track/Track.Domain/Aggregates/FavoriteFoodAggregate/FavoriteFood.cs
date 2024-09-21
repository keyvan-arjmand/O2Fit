using Track.Domain.Aggregates.FavoriteMealAggregate;
using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.FavoriteFoodAggregate;

public class FavoriteFood : AggregateRoot
{
    public string FoodId { get; set; } = string.Empty;
    public FavoriteFoodTranslation FoodName { get; set; } = new();
    public List<NotNegativeForDecimalTypes> NutrientValue { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}