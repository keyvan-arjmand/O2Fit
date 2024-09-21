namespace Identity.V2.Domain.ValueObjects;

[BsonSerializer(typeof(NotNegativeForDoubleTypesSerializer))]
public class NotNegativeForDoubleTypes: ValueObject
{

    public double Value { get; private set; }
    public NotNegativeForDoubleTypes()
    {
        
    }

    public NotNegativeForDoubleTypes(double value)
    {
        if (value < 0)
            throw new ValueMustBePositiveValueException("value can not be negative");
        Value = value;
    }
    public static implicit operator double(NotNegativeForDoubleTypes notNegativeForDoubleTypes) => notNegativeForDoubleTypes.Value;
    public static explicit operator NotNegativeForDoubleTypes(double value) => new(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}