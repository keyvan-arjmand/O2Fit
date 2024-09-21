namespace Track.Application.Dtos;

public class UserTrackWaterDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; }
    public decimal Value { get; set; }
    public string AppId { get; set; } = string.Empty;
}