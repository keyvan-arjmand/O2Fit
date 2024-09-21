namespace Food.V2.Domain.Serializers;

public class BarCodeGs1Serializer : IBsonSerializer<BarCodeGs1>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        var value = context.Reader.ReadString();
        return new BarCodeGs1(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BarCodeGs1 value)
    {
        context.Writer.WriteString(value);
    }

    public BarCodeGs1 Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new BarCodeGs1(value);    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is BarCodeGs1 barCodeGs1)
        {
            context.Writer.WriteString(barCodeGs1.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a BarCodeGs1");
        }
    }

    public Type ValueType => typeof(BarCodeGs1);
}