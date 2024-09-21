using Discount.Domain.ValueObjects;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace Discount.Domain.Serializers;

public class AmountValueSerializer : IBsonSerializer<AmountValue>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDouble();
        return new AmountValue(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, AmountValue value)
    {
        context.Writer.WriteDouble(value.Value);
    }

    public AmountValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDouble();
        return new AmountValue(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is AmountValue amount)
        {
            context.Writer.WriteDouble(amount.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a AmountValue");
        }
    }

    public Type ValueType => typeof(AmountValue);
}