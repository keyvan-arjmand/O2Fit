using Wallet.Application.Dtos;

namespace Wallet.Application.Wallets.V1.Query.GetWalletById;

public class GetWalletByIdQuery : IRequest<WalletDto>
{
    public string Id { get; set; } = string.Empty;
}