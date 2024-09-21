using Wallet.Domain.Common;

namespace Wallet.Domain.Aggregates.WalletAggregate;

public class Wallet : AggregateRoot
{
    public ObjectId UserId { get; set; }
    public double Amount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public ObjectId CurrencyId { get; set; }
    public string UserType { get; set; } = string.Empty;
    public int CountryId { get; set; }
}