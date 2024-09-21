using Common.Enums.TypeEnums;
using MongoDB.Bson;

namespace Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;

public class ChargeWalletTransactionCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string WalletId { get; set; } = string.Empty;
    public string? Bank { get; set; }
    public double Amount { get; set; }
    public string CurrencyId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string? SaleReferenceId { get; set; }
    public UserType UserType { get; set; }
    public PaymentType PaymentType { get; set; }
  
}