namespace Identity.V2.Domain.Aggregates.CountryAggregate;

public class Translation : BaseEntity
{
    public Translation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public string? Persian { get; set; }
    public string? English { get; set; }
    public string? Arabic { get; set; }
}