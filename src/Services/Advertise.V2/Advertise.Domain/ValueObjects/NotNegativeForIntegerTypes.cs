namespace Advertise.Domain.ValueObjects;

[BsonSerializer(typeof(NotNegativeForIntegerTypesSerializer))]
public class NotNegativeForIntegerTypes : ValueObject
{
    public int Value { get; private set; }
    public NotNegativeForIntegerTypes()
    {
        
    }

    public NotNegativeForIntegerTypes(int value)
    {
        if (value < 0)
            throw new ValueMustBePositiveValueForIntegerTypesException("value can not be negative");
        Value = value;
    }
    public static implicit operator int(NotNegativeForIntegerTypes nonNegativeForIntegerTypes) => nonNegativeForIntegerTypes.Value;
    public static explicit operator NotNegativeForIntegerTypes(int value) => new(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}