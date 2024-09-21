using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnCategoryAggregate;

public class AppLearnCategoryTranslation:BaseEntity
{
    public AppLearnCategoryTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }
  
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}