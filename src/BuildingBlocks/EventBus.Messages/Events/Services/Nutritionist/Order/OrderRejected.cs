namespace EventBus.Messages.Events.Services.Nutritionist.Order;

public record OrderRejected:BaseEvent
{
    public string OrderId { get; init; }
    public string UserId { get; init; }
    public string Username { get; init; }
}