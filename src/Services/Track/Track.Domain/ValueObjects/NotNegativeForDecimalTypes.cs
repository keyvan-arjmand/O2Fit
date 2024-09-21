using Track.Domain.Common;
using Track.Domain.Exceptions;
using Track.Domain.Serializers;

namespace Track.Domain.ValueObjects;

[BsonSerializer(typeof(NotNegativeForDecimalTypesSerializer))]
public class NotNegativeForDecimalTypes : ValueObject
{
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Value { get; private set; }

    public NotNegativeForDecimalTypes()
    {
    }

   
    public NotNegativeForDecimalTypes(decimal value)
    {
        if (value < 0)
            throw new ValueMustBePositiveForDecimalTypesValueException("value can not be negative");
        Value = value;
    }

    public static implicit operator decimal(NotNegativeForDecimalTypes notNegativeForDecimalTypesTypes) =>
        notNegativeForDecimalTypesTypes.Value;

    public static explicit operator NotNegativeForDecimalTypes(decimal value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}