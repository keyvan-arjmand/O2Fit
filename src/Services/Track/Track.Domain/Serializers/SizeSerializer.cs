using MongoDB.Bson.Serialization;
using Track.Domain.ValueObjects;
using NotSupportedException = System.NotSupportedException;

namespace Track.Domain.Serializers;

public class SizeSerializer : IBsonSerializer<Size>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadDouble();
        return new Size(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Size value)
    {
        context.Writer.WriteDouble((double)value.ValueSize);
    }

    public Size Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDouble();
        return new Size(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is Size size)
        {
            context.Writer.WriteDouble(size.ValueSize);
        }
        else
        {
            throw new NotSupportedException("This is not a ValueSize");
        }
    }

    public Type ValueType => typeof(Size);
}