namespace Identity.V2.Domain.Aggregates.SpecialDiseaseAggregate;

public class SpecialDiseaseTranslation : BaseEntity
{
    public SpecialDiseaseTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string? Persian { get; set; }
    public string? English { get; set; }
    public string? Arabic { get; set; }
}