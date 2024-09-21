namespace EventBus.Messages.Contracts.Services.Wallet;

public record GetWalletByUserId
{
    public string UserId { get; init; }
};