using Common.Enums.TypeEnums;

namespace EventBus.Messages.Contracts.Services.Discounts.DiscountCode.NutritionistCode;

public class CalculateNutritionistDiscountCode
{
    public string DiscountCode { get; init; } 
    public string PackageId { get; init; }
    public string UserId { get; init; }
    public string UserCurrencyCode { get; init; }
    public int CountryId { get; init; }
}