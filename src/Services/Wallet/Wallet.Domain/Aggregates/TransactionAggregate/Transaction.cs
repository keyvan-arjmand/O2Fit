using Wallet.Domain.Common;

namespace Wallet.Domain.Aggregates.TransactionAggregate;

public class Transaction : AggregateRoot
{
    public ObjectId UserId { get; set; }
    public ObjectId WalletId { get; set; }
    public ObjectId? PackageId { get; set; }
    public ObjectId CurrencyId { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string  TransactionType { get; set; } = string.Empty;
    public string PaymentFor { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public double Amount { get; set; }
   
}