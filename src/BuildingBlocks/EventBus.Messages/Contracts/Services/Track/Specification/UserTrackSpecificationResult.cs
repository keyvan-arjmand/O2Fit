namespace EventBus.Messages.Contracts.Services.Track.Specification;

public class UserTrackSpecificationResult
{
    public string Id { get; init; }
    public double Weight { get; init; }
    public double Neck { get; init; }
    public double Shoulder { get; init; }
    public double Arm { get; init; }
    public double Wrist { get; init; }
    public double Chest { get; init; }
    public double Waist { get; init; }
    public double HighHip { get; init; }
    public double Hip { get; init; }
    public double Thigh { get; init; }
    public DateTime InsertDate { get; init; }
    public string Note { get; init; }
    public string Image { get; init; }
    public string UserId { get; init; }
    public string AppId { get; init; }
}