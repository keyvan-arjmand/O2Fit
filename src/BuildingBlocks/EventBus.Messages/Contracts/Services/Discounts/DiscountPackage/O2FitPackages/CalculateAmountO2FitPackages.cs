namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;

public class CalculateAmountO2FitPackages
{
    public List<PackagePrice> Packages { get; init; }
    public string UserCurrencyCode { get; init; }
}

public class PackagePrice
{
    public string Id { get; init; }
    public double Price { get; init; }
    public string PackageCurrencyCode { get; init; }

}