using Common.Enums.TypeEnums;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageDietO2FitAggregate;

public class DietO2Fit:AggregateRoot
{
    public TranslationDietO2Fit TranslationName { get; set; } = new();
    public TranslationDietO2Fit TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public int Duration { get; set; }
    public string CurrencyCode { get; set; } = string.Empty; 
    public PackageType PackageType { get; set; } = PackageType.Diet;
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
}