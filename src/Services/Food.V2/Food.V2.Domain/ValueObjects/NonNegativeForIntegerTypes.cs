namespace Food.V2.Domain.ValueObjects;

[BsonSerializer(typeof(NotNegativeForIntegerTypesSerializer))]
public class NonNegativeForIntegerTypes : ValueObject
{
    public int Value { get; private set; }
    public NonNegativeForIntegerTypes()
    {
        
    }

    public NonNegativeForIntegerTypes(int value)
    {
        if (value < 0)
            throw new ValueMustBePositiveValueForIntegerTypesException("value can not be negative");
        Value = value;
    }
    public static implicit operator int(NonNegativeForIntegerTypes nonNegativeForIntegerTypes) => nonNegativeForIntegerTypes.Value;
    public static explicit operator NonNegativeForIntegerTypes(int value) => new(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}