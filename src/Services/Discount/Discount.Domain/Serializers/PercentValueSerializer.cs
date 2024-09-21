using Discount.Domain.ValueObjects;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace Discount.Domain.Serializers;

public class PercentValueSerializer: IBsonSerializer<PercentValue>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new PercentValue(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PercentValue value)
    {
        context.Writer.WriteInt32(value.Value);
    }

    public PercentValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new PercentValue(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is PercentValue amount)
        {
            context.Writer.WriteInt32(amount.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a PercentValue");
        }
    }

    public Type ValueType=> typeof(PercentValue);
}