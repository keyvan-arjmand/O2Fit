namespace Food.V2.Domain.Serializers;

public class BarCodeNationalSerializer : IBsonSerializer<BarCodeNational>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        var value = context.Reader.ReadString();
        return new BarCodeNational(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BarCodeNational value)
    {
        context.Writer.WriteString(value);
    }

    public BarCodeNational Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new BarCodeNational(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is BarCodeNational barCodeNational)
        {
            context.Writer.WriteString(barCodeNational.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a BarCodeNational");
        }
    }

    public Type ValueType => typeof(BarCodeGs1);
}