namespace Track.Application.Dtos;

public class SleepValueDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime? StartDate  { get; set; }
    public DateTime? EndDate  { get; set; }
    public TimeSpan Duration { get; set; }
    public double DailyAverageRate { get; set; }
    public string AppId { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}