namespace EventBus.Messages.Contracts.Services.Wallet;

public class GetWalletByUserIdResult
{
    public string Id { get; init; }
    public string CurrencyCode { get; init; }
    public string CurrencyId { get; init; }
    public double Amount { get; init; }
}