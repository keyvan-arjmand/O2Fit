namespace EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;

public class CalculateAmountO2FitPackageById
{
    public string Id { get; init; }
    public double Price { get; init; }
    public string PackageCurrencyCode { get; init; }
    public string UserCurrencyCode { get; init; }
}