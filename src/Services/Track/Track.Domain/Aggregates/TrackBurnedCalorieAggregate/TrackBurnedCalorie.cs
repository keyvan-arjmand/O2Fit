using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackBurnedCalorieAggregate;

public class TrackBurnedCalorie:AggregateRoot
{
    public DateTime InsertDate { get; set; }
    public NotNegativeForDecimalTypes Value { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}