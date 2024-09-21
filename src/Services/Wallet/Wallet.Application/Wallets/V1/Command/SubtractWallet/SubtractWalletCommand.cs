using Common.Enums.TypeEnums;

namespace Wallet.Application.Wallets.V1.Command.SubtractWallet;

public class SubtractWalletCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public double SubtractAmount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string TransactionCompId { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;
}