namespace EventBus.Messages.Events.Services.Identity;

public record UserProfileCreated : BaseEvent
{
    public string UserId { get; init; }
}