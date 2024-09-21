using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Wallet;

public record WalletTransactionCreatedEvent : BaseEvent
{
    //when success payment
    public string PackageId { get; init; }
    public string TransactionId { get; init; }
    public string Username { get; init; }
    public UserType UserType { get; init; }
    public PaymentType PaymentType { get; init; }
}