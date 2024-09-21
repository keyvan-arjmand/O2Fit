using Common.Enums.TypeEnums;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.CreateTransactionWallet;

public class CreateTransactionWalletCommand : IRequest<long>
{
    public Bank Bank { get; set; }
    public UserType UserType { get; set; }
    public double AmountCharge { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int CountryId { get; set; }
}