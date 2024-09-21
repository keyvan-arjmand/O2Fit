using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.FoodCategories.V1.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string? ParentId { get; set; }
    public double? Percent { get; set; }
    public TranslationDto Translation { get; set; } = new();
}