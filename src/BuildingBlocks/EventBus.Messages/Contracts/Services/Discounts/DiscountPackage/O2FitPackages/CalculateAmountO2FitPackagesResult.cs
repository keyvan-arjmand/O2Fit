namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;

public class CalculateAmountO2FitPackagesResult
{
    public List<PackageDiscount> Packages { get; init; } = new List<PackageDiscount>();
}

public class PackageDiscount
{
    public string Id { get; init; }
    public double DiscountPrice { get; init; }
    public double Wage { get; init; }
}