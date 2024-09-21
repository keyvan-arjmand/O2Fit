using MongoDB.Bson;

namespace Wallet.Application.Dtos;

public class WalletDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyId { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public int CountryId { get; set; }
}