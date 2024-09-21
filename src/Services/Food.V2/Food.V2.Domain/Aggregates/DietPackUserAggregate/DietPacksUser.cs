namespace Food.V2.Domain.Aggregates.DietPackUserAggregate;

public class DietPacksUser: AggregateRoot
{
    public List<DietPackUser> DietPacks { get; set; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ObjectId Type { get; set; }
}