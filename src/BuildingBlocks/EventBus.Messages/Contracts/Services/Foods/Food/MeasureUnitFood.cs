namespace EventBus.Messages.Contracts.Services.Foods.Food;

public class MeasureUnitFood
{
    public string Id { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string MeasureUnitPersian { get; init; }
    public string MeasureUnitEnglish { get; init; }
    public string MeasureUnitArabic { get; init; }
}