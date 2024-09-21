using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackNutrientAggregate;

public class TrackNutrient : AggregateRoot
{
    public DateTime InsertDate { get; set; }
    public List<NotNegativeForDecimalTypes> Value { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}