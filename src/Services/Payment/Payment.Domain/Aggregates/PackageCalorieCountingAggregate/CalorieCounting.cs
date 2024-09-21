using Common.Enums.TypeEnums;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageCalorieCountingAggregate;

public class CalorieCounting : AggregateRoot
{
    public TranslationCalorieCounting TranslationName { get; set; } = new();
    public TranslationCalorieCounting TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public int Duration { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public PackageType PackageType { get; set; } = PackageType.CalorieCounting;
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
}