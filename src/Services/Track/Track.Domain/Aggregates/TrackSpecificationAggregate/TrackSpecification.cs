using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackSpecificationAggregate;

public class TrackSpecification : AggregateRoot
{
    public double? Weight { get; set; }
    public double? Neck { get; set; } 
    public double? Shoulder { get; set; }
    public double? Arm { get; set; } 
    public double? Wrist { get; set; } 
    public double? Chest { get; set; }
    public double? Waist { get; set; } 
    public double? HighHip { get; set; } 
    public double? Hip { get; set; }
    public double? Thigh { get; set; } 
    // [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime InsertDate { get; set; }
    public string? Note { get; set; }
    public string? Image { get; set; }
    public string AppId { get; set; } = string.Empty;
}