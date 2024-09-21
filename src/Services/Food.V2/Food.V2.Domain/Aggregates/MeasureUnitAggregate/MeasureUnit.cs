namespace Food.V2.Domain.Aggregates.MeasureUnitAggregate;

public class MeasureUnit : AggregateRoot
{
    public MeasureUnit()
    {
        
    }

    public MeasureUnit(NotNegativeForDecimalTypes value, bool isActive, MeasureUnitTranslation translation)
    {
        Value = value;
        IsActive = isActive;
        Translation = translation;
    }
    public NotNegativeForDecimalTypes Value { get; set; }
    public bool IsActive { get; set; }
    public MeasureUnitTranslation Translation { get; set; } = null!;
}