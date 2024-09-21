namespace Payment.Application.Transactions.V1.Command.UpdateTransactionMyket;

public class UpdateTransactionMyketCommand:IRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string SaleReferenceId { get; set; } = string.Empty;
}