namespace Notification.Application.Dtos;

public class MessageLogDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string ToPhoneNumber { get; set; } = string.Empty;
    public string ToFcmToken { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}