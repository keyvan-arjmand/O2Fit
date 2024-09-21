namespace Food.V2.Application.Dtos.MeasureUnits;

public class MeasureUnitDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public MeasureUnitTranslationDto Name { get; set; } = null!;
}