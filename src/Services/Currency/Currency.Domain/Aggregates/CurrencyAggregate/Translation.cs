using Currency.Domain.Common;

namespace Currency.Domain.Aggregates.CurrencyAggregate;

public class Translation : BaseEntity
{
    public Translation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}