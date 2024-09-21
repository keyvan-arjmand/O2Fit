using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageDietO2FitAggregate;

public class TranslationDietO2Fit: BaseEntity
{
    public TranslationDietO2Fit()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}