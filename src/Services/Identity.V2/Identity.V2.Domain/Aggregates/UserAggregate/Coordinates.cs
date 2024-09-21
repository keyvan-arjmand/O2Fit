namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class Coordinates : BaseEntity
{
    public Coordinates()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

   
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
}