using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackStepAggregate;

public class TrackStep:AggregateRoot
{
    public int UserId { get; set; } 
    public DateTime InsertDate { get; set; }
    public int StepsCount { get; set; }
    public NotNegativeForDecimalTypes BurnedCalories { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
    public bool IsManual { get; set; }
}