using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.Dtos.FoodCategory;

public class FoodCategoryDto
{
    public string Id { get; set; } = string.Empty;
    public string? ParentId { get; set; }

    public double? Percent { get; set; }

    public TranslationDto Translation { get; set; } = new();
    public TranslationDto ParentTranslation { get; set; } = new ();
}