namespace Food.V2.Domain.Aggregates.FoodAggregate;

public class FoodTranslation : BaseEntity
{
    public FoodTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}