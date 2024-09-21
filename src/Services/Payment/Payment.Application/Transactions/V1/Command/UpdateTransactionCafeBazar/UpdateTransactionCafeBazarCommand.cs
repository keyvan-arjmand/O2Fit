using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionCafeBazar;

public class UpdateTransactionCafeBazarCommand : IRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string SaleReferenceId { get; set; } = string.Empty;
}