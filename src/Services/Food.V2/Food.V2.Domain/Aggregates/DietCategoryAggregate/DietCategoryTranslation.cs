namespace Food.V2.Domain.Aggregates.DietCategoryAggregate;

public class DietCategoryTranslation:BaseEntity
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}