namespace Notification.Application.Dtos;

public class MessageRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string DeviceToken { get; set; } = string.Empty;
}