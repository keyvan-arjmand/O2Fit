namespace Food.V2.Domain.Aggregates.FaultFoodAggregate;

public class FaultFoodTranslation : BaseEntity
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}