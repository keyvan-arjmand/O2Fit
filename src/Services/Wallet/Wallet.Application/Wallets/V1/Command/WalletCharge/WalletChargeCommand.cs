using Common.Enums.TypeEnums;

namespace Wallet.Application.Wallets.V1.Command.WalletCharge;

public class WalletChargeCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public PaymentType PaymentType { get; set; }
    public PackageType PackageType { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public int? Duration { get; set; }
}