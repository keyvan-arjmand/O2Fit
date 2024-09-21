using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Wallet;

public record WalletChargedNutritionistEvent : BaseEvent
{
    //when order accepted
    public string NutritionistId { get; init; }
    public string TransactionId { get; init; }
    public string OrderId { get; init; }
    public string Username { get; init; }
}