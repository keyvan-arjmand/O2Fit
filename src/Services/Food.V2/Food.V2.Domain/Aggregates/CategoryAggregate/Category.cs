namespace Food.V2.Domain.Aggregates.CategoryAggregate;

public class Category : AggregateRoot
{
    public int? OldSystemId { get; set; }
    public int? OldSystemParentId { get; set; }
    public ObjectId? ParentId { get; set; }
    public double? Percent { get; set; }
    public CategoryTranslation Translation { get; set; } = new();
}