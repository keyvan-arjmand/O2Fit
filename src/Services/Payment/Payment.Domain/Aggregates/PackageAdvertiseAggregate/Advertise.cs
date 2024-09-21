using Common.Enums.TypeEnums;
using Payment.Domain.Aggregates.TransactionDietNutritionistPackageAggregate;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageAdvertiseAggregate;

public class Advertise : AggregateRoot
{
    public TransactionDietNutritionist TranslationTitle { get; set; } = new();
    public TransactionDietNutritionist? TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public List<int> CountryIds { get; set; } = new();
    public double Cost { get; set; } //per click
    public double ClickCount { get; set; }
    public PackageType PackageType { get; set; } = PackageType.Advertise;
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public AdvertisePackageType AdvertisePackageType { get; set; }
}