using Track.Domain.Common;
using Track.Domain.Enums;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackFoodAggregate;

public class TrackFood : AggregateRoot
{
    public NotNegativeForDecimalTypes Value { get; set; }= new();
    public FoodMeal FoodMeal { get; set; }
    public DateTime InsertDate { get; set; }
    public List<NotNegativeForDecimalTypes> FoodNutrientValue { get; set; } = new();
    public ObjectId MeasureUnitId { get; set; }
    public string MeasureUnitName { get; set; } = string.Empty;
    public ObjectId? PersonalFoodId { get; set; }
    public ObjectId? FoodId { get; set; }
    public string  FoodName { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
}