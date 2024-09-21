namespace EventBus.Messages.Contracts.Services.Foods.MeasureUnit;

public class GetMeasureUnitResult
{
    public decimal value { get; init; }
    public Translation Name { get; init; }
}