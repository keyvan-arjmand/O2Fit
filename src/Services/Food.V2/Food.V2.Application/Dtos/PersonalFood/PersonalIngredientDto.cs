using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.PersonalFood;

public class PersonalIngredientDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = new();
    public decimal Value { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    // public TranslationDto MeasureUnitName { get; set; }
    public List<string> MeasureUnitList { get; set; } = new();
}