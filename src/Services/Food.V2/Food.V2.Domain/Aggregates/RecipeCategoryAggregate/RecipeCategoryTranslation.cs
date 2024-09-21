namespace Food.V2.Domain.Aggregates.RecipeCategoryAggregate;

public class RecipeCategoryTranslation : BaseEntity
{
    public RecipeCategoryTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}