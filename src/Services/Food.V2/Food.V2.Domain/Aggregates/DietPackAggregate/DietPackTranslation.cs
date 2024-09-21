namespace Food.V2.Domain.Aggregates.DietPackAggregate;

public class DietPackTranslation : BaseEntity
{
    public DietPackTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}