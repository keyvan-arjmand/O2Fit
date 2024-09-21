namespace EventBus.Messages.Events.Services.Wallet;

public record NutritionistCheckoutNotification : BaseEvent
{
    public string UserId { get; init; }
    public decimal Amount { get; init; }
}