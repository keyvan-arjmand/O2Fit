using Discount.Domain.Common;

namespace Discount.Domain.Aggregates.CurrencyAggregate;

public class CurrencyTranslation : BaseEntity
{
    public CurrencyTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}