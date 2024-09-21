namespace Food.V2.Domain.Aggregates.MissingFoodAggregate;

public class MissingFood: AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; }= string.Empty;
    public string FirstImageName { get; set; }= string.Empty;
    public string SecondImageName { get; set; }= string.Empty;
}