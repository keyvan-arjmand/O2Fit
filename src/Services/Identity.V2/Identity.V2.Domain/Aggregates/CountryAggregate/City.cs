namespace Identity.V2.Domain.Aggregates.CountryAggregate;

public class City : BaseEntity
{
    public City()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public Translation Name { get; set; } = default!;
}