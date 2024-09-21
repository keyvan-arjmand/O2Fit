using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Commands.CreateDietCategory;

public class CreateDietCategoryCommand : IRequest<string>
{
    public TranslationDto Name { get; set; } = null!;
    public TranslationDto Description { get; set; } = null!;
    public string? ImageName { get; set; }
    public string? BannerImageName { get; set; }
    public bool IsActive { get; set; }
    public bool IsPromote { get; set; }
    public string ParentId { get; set; } = string.Empty;
}