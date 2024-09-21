namespace Food.V2.Domain.Aggregates.MeasureUnitAggregate;

public class MeasureUnitTranslation : BaseEntity
{
    public MeasureUnitTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}