using Common.Enums.TypeEnums;

namespace EventBus.Messages.Contracts.Services.Discounts.DiscountCode.O2FitCode;

public class CalculateO2FitDiscountCode
{
    public string DiscountCode { get; set; } 
    public string PackageId { get; set; }
    public string UserId { get; set; }
    public string UserCurrencyCode { get; set; }
    public int CountryId { get; set; }
}