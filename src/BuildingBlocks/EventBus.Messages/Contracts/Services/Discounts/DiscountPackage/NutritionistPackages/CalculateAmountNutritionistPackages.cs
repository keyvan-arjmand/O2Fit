namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;

public class CalculateAmountNutritionistPackages
{
    public List<PackageNutritionistPrice> Packages { get; init; }
    public string UserCurrencyCode { get; init; }
}

public class PackageNutritionistPrice
{
    public string Id { get; init; }
    public double Price { get; init; }
    public string PackageCurrencyCode { get; init; }
}