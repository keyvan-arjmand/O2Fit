using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Wallet;

public record WalletChargedUserEvent:BaseEvent
{
    //when order rejected
    public string TransactionId { get; init; }
    public string OrderId { get; init; }
    public string Username { get; init; }
}