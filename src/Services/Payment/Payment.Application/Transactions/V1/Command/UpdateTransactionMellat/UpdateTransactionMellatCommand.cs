using Payment.Application.Dtos.Mellat;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionMellat;

public class UpdateTransactionMellatCommand:IRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public MellatResult MellatResult { get; set; } = default!;
    public PaymentResult Status { get; set; }
}