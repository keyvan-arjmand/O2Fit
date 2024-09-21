namespace Food.V2.Domain.Aggregates.DietPackNutritionistAggregate;

public class DietPackNutritionistTranslation : BaseEntity
{
    public DietPackNutritionistTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}