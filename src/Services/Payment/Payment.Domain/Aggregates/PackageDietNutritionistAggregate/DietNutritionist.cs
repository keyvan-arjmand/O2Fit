using Common.Enums.TypeEnums;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

public class DietNutritionist : AggregateRoot
{
    public TranslationDietNutritionist TranslationName { get; set; } = new();
    public TranslationDietNutritionist TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public int Duration { get; set; }
    public string CurrencyCode { get; set; } = string.Empty; 
    public ObjectId DietCategoryId { get; set; }
    public TranslationDietNutritionist DietCategoryName { get; set; } = new();
    public PackageType PackageType { get; set; } = PackageType.NutritionistDietPack;
    public bool IsActive { get; set; }
}