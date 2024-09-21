namespace Food.V2.Domain.Aggregates.DietPackUserAggregate;

public class DietPackUserTranslation : BaseEntity
{
    public DietPackUserTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}