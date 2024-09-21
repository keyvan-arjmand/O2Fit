using Discount.Domain.Common;

namespace Discount.Domain.Aggregates.DiscountO2FitAggregate;

public class DiscountO2FitTranslation : BaseEntity
{
    public DiscountO2FitTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}