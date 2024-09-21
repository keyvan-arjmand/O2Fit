using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class MeasureUnitsDto
{
    public string Id { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public bool IsActive { get; set; }
    public TranslationDto Name { get; set; } = null!;
}