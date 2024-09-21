namespace Track.Application.Dtos;

public class UserStepsDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; }
    public int StepsCount { get; set; }
    public double BurnedCalories { get; set; }
    public string AppId { get; set; } = string.Empty;
    public bool IsManual { get; set; }
}