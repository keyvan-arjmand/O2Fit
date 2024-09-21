using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnSubCategoryAggregate;

public class AppLearnSubCategoryTranslation:BaseEntity
{
    public AppLearnSubCategoryTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }
  
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}