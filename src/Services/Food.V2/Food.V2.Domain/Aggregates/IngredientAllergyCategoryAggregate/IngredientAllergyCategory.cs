namespace Food.V2.Domain.Aggregates.IngredientAllergyCategoryAggregate;

public class IngredientAllergyCategory : AggregateRoot
{
    public ObjectId IngredientId { get; set; }
    public List<ObjectId> IngredientAllergiesIds { get; set; } = new();
}