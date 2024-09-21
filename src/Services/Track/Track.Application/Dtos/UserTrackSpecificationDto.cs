using MongoDB.Bson;

namespace Track.Application.Dtos;

public class UserTrackSpecificationDto
{
    public string Id { get; set; } = string.Empty;
    public double Weight { get; set; }
    public double Neck { get; set; }
    public double Shoulder { get; set; }
    public double Arm { get; set; }
    public double Wrist { get; set; }
    public double Chest { get; set; }
    public double Waist { get; set; }
    public double HighHip { get; set; }
    public double Hip { get; set; }
    public double Thigh { get; set; }
    public DateTime InsertDate { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
}