using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageCalorieCountingAggregate;

public class TranslationCalorieCounting: BaseEntity
{
    public TranslationCalorieCounting()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
    
}