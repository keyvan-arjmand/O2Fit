namespace Identity.V2.Domain.ValueObjects;

[BsonSerializer(typeof(WeightTimeRangeSerializer))]
public class WeightTimeRange : ValueObject
{
    public int Value { get; set; }

    public WeightTimeRange()
    {
        
    }

    public WeightTimeRange(int value)
    {
        if (value < 0)
            throw new WeightTimeRangeCannotBeNegativeException("Weight time rage can not be negative");
        if (value > 999)
            throw new WeightTimeRangeMaxValueException("Max value of weight time rage is 999");
        Value = value;
    }
    public static implicit operator int(WeightTimeRange weightTimeRange) => weightTimeRange.Value;
    public static explicit operator WeightTimeRange(int value) => new(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}