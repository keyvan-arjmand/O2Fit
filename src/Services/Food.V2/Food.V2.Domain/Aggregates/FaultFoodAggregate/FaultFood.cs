namespace Food.V2.Domain.Aggregates.FaultFoodAggregate;

public class FaultFood : AggregateRoot
{
    public ObjectId FoodId { get; set; }
    public string Description { get; set; } = string.Empty;
    public ReasonsProblem ProblemType { get; set; }
}