using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Ingredients;

public class IngredientMeasureUnitDto
{
    public string IngredientId { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    public TranslationDto Translation { get; set; } = new();

}