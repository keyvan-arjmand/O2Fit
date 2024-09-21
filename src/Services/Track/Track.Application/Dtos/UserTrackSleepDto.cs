namespace Track.Application.Dtos;

public class UserTrackSleepDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int Rate { get; set; }
    public DateTime EndDate { get; set; }//InsertDate
    public string Duration { get; set; } = string.Empty;
    public double BurnedCalories { get; set; }
    public string AppId { get; set; } = string.Empty;
    public DateTime? InsertDate { get; set; }
}