namespace EventBus.Messages.Contracts.Services.Payments.Transaction;

public class GetTransactionByBankOrderId
{
    public long BankOrderId { get; init; }
}