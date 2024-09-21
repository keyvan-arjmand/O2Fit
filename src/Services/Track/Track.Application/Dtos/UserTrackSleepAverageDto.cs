namespace Track.Application.Dtos;

public class UserTrackSleepAverageDto
{
    public string UserId { get; set; } = string.Empty;
    public double AverageRate { get; set; }
    public decimal AverageBurnedCalories { get; set; }
    public TimeSpan AverageSleepDuration { get; set; }
    public List<SleepValueDto> SleepDetails { get; set; } = new();
}