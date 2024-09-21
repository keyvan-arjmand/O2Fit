using Wallet.Application.Dtos;

namespace Wallet.Application.TransactionCompanies.V1.Query.GetTransactionCompanyById;

public class GetTransactionCompanyByIdCommand : IRequest<TransactionCompDto>
{
    public string Id { get; set; } = string.Empty;
}