using Payment.Application.Dtos;

namespace Payment.Application.Transactions.V1.Query.GetById;

public class GetByIdTransactionQuery : IRequest<TransactionDto>
{
    public string Id { get; set; } = string.Empty;
}