namespace Food.V2.Domain.Aggregates.IngredientAggregate;

public class IngredientTranslation: BaseEntity
{
    public IngredientTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}