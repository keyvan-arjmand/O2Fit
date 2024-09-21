namespace Identity.V2.Domain.Aggregates.CountryAggregate;

public class State : BaseEntity
{
    public State()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public Translation Name { get; set; } = default!;
    public List<City> Cities { get; set; } = new();
}