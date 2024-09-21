using Common.Enums.TypeEnums;
using Payment.Application.Dtos.Mellat;
using Payment.Application.Dtos.Saman;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.CreateTransaction;

public class    CreateTransactionCommand : IRequest<long>
{
    public Bank Bank { get; set; }
    public UserType UserType { get; set; }
    public string PackageId { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int CountryId { get; set; } 
}