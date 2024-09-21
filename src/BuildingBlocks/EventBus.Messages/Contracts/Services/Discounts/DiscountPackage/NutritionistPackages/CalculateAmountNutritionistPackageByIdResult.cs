namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;

public class CalculateAmountNutritionistPackageByIdResult
{
    public string Id { get; init; }
    public double DiscountPrice { get; init; }
    public double Wage { get; init; }

}