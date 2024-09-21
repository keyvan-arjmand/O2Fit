using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.PersonalFood;

public class IngredientDto
{
    public string Id { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    public TranslationDto Translation { get; set; } = new();
}