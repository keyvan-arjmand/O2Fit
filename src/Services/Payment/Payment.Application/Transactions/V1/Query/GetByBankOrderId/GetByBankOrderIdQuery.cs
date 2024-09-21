using Payment.Application.Dtos;

namespace Payment.Application.Transactions.V1.Query.GetByBankOrderId;

public class GetByBankOrderIdQuery : IRequest<TransactionDto>
{
    public long BankOrderId { get; set; }
}