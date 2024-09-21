using Market.Domain.Common;

namespace Market.Domain.Aggregates.FAQAggregate;

public class FaqTranslation:BaseEntity
{
    public FaqTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }
  
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}