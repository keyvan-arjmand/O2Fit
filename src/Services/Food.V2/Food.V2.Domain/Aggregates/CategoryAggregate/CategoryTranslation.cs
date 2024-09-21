namespace Food.V2.Domain.Aggregates.CategoryAggregate;

public class CategoryTranslation:BaseEntity
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}