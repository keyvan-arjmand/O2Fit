using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Commands.CreateCategory;

public class CreateCategoryCommand:IRequest<string>
{
    public string? ParentId { get; set; }
    
    public double? Percent { get; set; }

    public TranslationDto Translation { get; set; } = new();
}