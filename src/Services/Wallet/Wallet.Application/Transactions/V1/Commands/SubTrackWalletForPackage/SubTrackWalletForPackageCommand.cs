using Common.Enums.TypeEnums;

namespace Wallet.Application.Transactions.V1.Commands.SubTrackWalletForPackage;

public class SubTrackWalletForPackageCommand:IRequest<string>
{
    public string PackageId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public double Amount { get; set; }
    public UserType UserType { get; set; }
    public PaymentType PaymentType { get; set; }

}