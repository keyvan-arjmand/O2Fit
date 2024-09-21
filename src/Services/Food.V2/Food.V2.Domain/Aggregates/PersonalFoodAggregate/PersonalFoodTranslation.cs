namespace Food.V2.Domain.Aggregates.PersonalFoodAggregate;

public class PersonalFoodTranslation:BaseEntity
{
    public PersonalFoodTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}