using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnAggregate;

public class AppLearnTranslation:BaseEntity
{
    public AppLearnTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}