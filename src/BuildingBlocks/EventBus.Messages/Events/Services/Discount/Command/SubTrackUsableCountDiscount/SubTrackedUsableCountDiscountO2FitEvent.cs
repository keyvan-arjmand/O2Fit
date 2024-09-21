namespace EventBus.Messages.Events.Services.Discount.Command.SubTrackUsableCountDiscount;

public record SubTrackedUsableCountDiscountO2FitEvent : BaseEvent
{
    public string DiscountCode { get; init; }
    public string UserId { get; init; }
    public string Username { get; init; }
}