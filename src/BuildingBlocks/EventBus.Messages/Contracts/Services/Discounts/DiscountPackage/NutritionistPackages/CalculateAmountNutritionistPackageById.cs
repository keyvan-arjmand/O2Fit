namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;

public class CalculateAmountNutritionistPackageById
{
    public string Id { get; init; }
    public double Price { get; init; }
    public string PackageCurrencyCode { get; init; }
    public string UserCurrencyCode { get; init; }
}