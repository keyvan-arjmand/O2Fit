using Discount.Domain.ValueObjects;
using MongoDB.Bson.Serialization;

namespace Discount.Domain.Serializers;

public class DiscountCodeSerializer : IBsonSerializer<DiscountCode>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new DiscountCode(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DiscountCode value)
    {
        context.Writer.WriteString(value.Code);
    }

    public DiscountCode Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new DiscountCode(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is DiscountCode discountCode)
        {
            context.Writer.WriteString(discountCode.Code);
        }
        else
        {
            throw new NotSupportedException("This is not a CurrencyType");
        }
    }

    public Type ValueType => typeof(DiscountCode);
}