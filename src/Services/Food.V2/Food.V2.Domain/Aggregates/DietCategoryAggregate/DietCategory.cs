namespace Food.V2.Domain.Aggregates.DietCategoryAggregate;

public class DietCategory : AggregateRoot
{
    public DietCategoryTranslation Name { get; set; } = null!;
    public DietCategoryTranslation Description { get; set; } = null!;
    public string? ImageName { get; set; }
    public string? BannerImageName { get; set; }
    public bool IsActive { get; set; }
    public bool IsPromote { get; set; }
    public ObjectId? ParentId { get; set; }
}