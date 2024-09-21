namespace EventBus.Messages.Events.Services.Order;

public record OrderCreatedEvent : BaseEvent
{
    //when Subtract Wallet
    public string WalletTransactionId { get; init; }
    public string PaymentTransactionId { get; init; }
    public string UserId { get; init; }
    public string Username { get; init; }
}