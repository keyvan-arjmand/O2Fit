namespace EventBus.Messages.Events.Services.Chat;

public record ChatCreated : BaseEvent
{
    public string UserId { get; init; }
    public string NutritionistId { get; init; }
    public string UserFullName { get; init; }
    public string NutritionistFullName { get; init; }

    public string OrderId { get; init; }
}