namespace Track.Application.Dtos;

public class NutrientDto
{
    public string Text { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
    public int Value { get; set; }
    public decimal MeasureUnitValue { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;
}