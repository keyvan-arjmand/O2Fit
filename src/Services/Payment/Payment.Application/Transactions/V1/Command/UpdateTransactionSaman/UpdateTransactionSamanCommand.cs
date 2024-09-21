using Payment.Application.Dtos.Mellat;
using Payment.Application.Dtos.Saman;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;

public class UpdateTransactionSamanCommand : IRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public SamanResult SamanResult { get; set; } = default!;
    public PaymentResult Status { get; set; }
}