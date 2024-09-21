namespace Track.Application.TrackSpecifications.V1.Command.InsertUserTrackSpecification;

public class InsertUserTrackSpecificationCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
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
    public string? Note { get; set; }
    public string? Image { get; set; }
    public string AppId { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; }
}