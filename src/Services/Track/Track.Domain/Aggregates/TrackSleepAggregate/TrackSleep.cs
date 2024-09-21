using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackSleepAggregate;

public class TrackSleep : AggregateRoot
{
    public DateTime InsertDate { get; set; }
    public int Rate { get; set; }
    public TimeSpan Duration { get; set; }
    public NotNegativeForDecimalTypes BurnedCalories { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}