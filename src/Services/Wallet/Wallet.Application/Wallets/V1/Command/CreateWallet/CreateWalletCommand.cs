using Common.Enums.TypeEnums;

namespace Wallet.Application.Wallets.V1.Command.CreateWallet;

public class CreateWalletCommand:IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int CountryId { get; set; } 
    public string CurrencyCode { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}