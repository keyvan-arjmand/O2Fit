using Market.Domain.Common;

namespace Market.Domain.Aggregates.MarketMessageAggregate;

public class TranslationMarketMessage:BaseEntity
{
    public TranslationMarketMessage()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}