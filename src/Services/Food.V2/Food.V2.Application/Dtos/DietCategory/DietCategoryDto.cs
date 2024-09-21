using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.Dtos.DietCategory;

public class DietCategoryDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = new();
    public TranslationDto ParentName { get; set; } = new();
    public TranslationDto Description { get; set; } = new();
    public TranslationDto ParentDescription { get; set; } = new();
    public string? ImageName { get; set; }
    public string? BannerImageName { get; set; }
    public bool IsActive { get; set; }
    public bool IsPromote { get; set; }
    public string ParentId { get; set; } = string.Empty;
}