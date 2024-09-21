namespace Food.V2.Domain.Aggregates.RecipeAggregate;

public class RecipeTranslation : BaseEntity
{
    public RecipeTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}