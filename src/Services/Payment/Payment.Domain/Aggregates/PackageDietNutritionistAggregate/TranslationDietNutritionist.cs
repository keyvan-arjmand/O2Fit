using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

public class TranslationDietNutritionist: BaseEntity
{
    public TranslationDietNutritionist()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}