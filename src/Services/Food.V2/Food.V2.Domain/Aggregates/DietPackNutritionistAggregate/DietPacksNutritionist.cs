namespace Food.V2.Domain.Aggregates.DietPackNutritionistAggregate;

public class DietPacksNutritionist: AggregateRoot
{
    public List<DietPackNutritionist> DietPacks { get; set; } = new();
    
}