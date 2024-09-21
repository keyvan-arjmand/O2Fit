namespace EventBus.Messages.Events.Services.Notification;

public record PushedNotification : BaseEvent
{
    public string UserId { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
};