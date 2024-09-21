using Discount.Domain.Common;
using Discount.Domain.Exceptions.Percent;
using Discount.Domain.Serializers;

namespace Discount.Domain.ValueObjects;
[BsonSerializer(typeof(PercentValueSerializer))]
public class PercentValue : ValueObject
{
    public PercentValue(int amount)
    {
        if (amount < 0) throw new PercentValidationException("The value cannot be negative");
        if (amount > 100) throw new PercentValidationException("Not Valid Value");
    }
    public int Value { get; private set; }
    public static implicit operator double(PercentValue amount) => amount.Value;
    public static explicit operator PercentValue(int value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToString();
    }
}