using Discount.Domain.Common;
using Discount.Domain.Exceptions.Amount;
using Discount.Domain.Serializers;

namespace Discount.Domain.ValueObjects;

[BsonSerializer(typeof(AmountValueSerializer))]
public class AmountValue : ValueObject
{
    public AmountValue(double amount)
    {
        if (amount < 0) throw new AmountValidationException("The value cannot be negative");
        if (amount > 50) throw new AmountValidationException("Not Valid Value");
    }

    public double Value { get; private set; }
    public static implicit operator double(AmountValue amount) => amount.Value;
    public static explicit operator AmountValue(double value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToString();
    }
}