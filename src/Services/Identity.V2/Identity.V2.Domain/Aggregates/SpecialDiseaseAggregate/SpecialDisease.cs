namespace Identity.V2.Domain.Aggregates.SpecialDiseaseAggregate;

public class SpecialDisease : AggregateRoot
{
    public SpecialDiseaseTranslation Name { get; set; } = null!;
}