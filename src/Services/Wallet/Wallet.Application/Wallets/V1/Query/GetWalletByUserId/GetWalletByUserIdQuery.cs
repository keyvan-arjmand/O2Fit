using Wallet.Application.Dtos;

namespace Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

public class GetWalletByUserIdQuery : IRequest<WalletDto>
{
    public string UserId { get; set; } = string.Empty;
}