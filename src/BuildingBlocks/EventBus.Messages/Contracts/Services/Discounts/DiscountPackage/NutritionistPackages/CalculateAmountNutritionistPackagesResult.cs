namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;

public class CalculateAmountNutritionistPackagesResult
{
    public List<PackageNutritionistDiscount> Packages { get; init; } = new List<PackageNutritionistDiscount>();
    public double Rate { get; init; }
}

public class PackageNutritionistDiscount
{
    public string Id { get; init; }
    public double DiscountPrice { get; init; }
    public double Wage { get; init; }

}