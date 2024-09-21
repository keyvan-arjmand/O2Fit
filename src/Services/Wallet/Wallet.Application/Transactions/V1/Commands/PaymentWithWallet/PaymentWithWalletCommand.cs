namespace Wallet.Application.Transactions.V1.Commands.PaymentWithWallet;

public class PaymentWithWalletCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string PackageId { get; set; } = string.Empty;
}