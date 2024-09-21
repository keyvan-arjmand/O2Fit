namespace Food.V2.Domain.Aggregates.NationalityAggregate;

public class Nationality : AggregateRoot
{
    public NationalityTranslation Translation { get; set; } = new();
    public ObjectId? ParentId { get; set; }
    public int? OldSystemId { get; set; }
    public int? OldSystemParentId { get; set; }
}