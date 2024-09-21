using Discount.Domain.Common;

namespace Discount.Domain.Aggregates.DiscountNutritionistAggregate;

public class DiscountNutritionistTranslation : BaseEntity
{
    public DiscountNutritionistTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}