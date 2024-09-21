namespace Identity.V2.Domain.Serializers;

public class HeightSizeSerializer : IBsonSerializer<HeightSize>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadInt32();
        return new HeightSize(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, HeightSize value)
    {
        context.Writer.WriteInt32(value.Value);
    }

    public HeightSize Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new HeightSize(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is HeightSize heightSize)
        {
            context.Writer.WriteInt32(heightSize.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a HeightSize");
        }
    }

    public Type ValueType => typeof(HeightSize);
}