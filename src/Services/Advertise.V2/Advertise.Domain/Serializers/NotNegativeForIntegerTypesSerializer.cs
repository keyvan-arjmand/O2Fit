namespace Advertise.Domain.Serializers;

public class NotNegativeForIntegerTypesSerializer: IBsonSerializer<NotNegativeForIntegerTypes>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {  
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadInt32();
        return new NotNegativeForIntegerTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        NotNegativeForIntegerTypes value)
    {
        context.Writer.WriteInt32(value);
    }

    public NotNegativeForIntegerTypes Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new NotNegativeForIntegerTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is NotNegativeForIntegerTypes integerTypes)
        {
            context.Writer.WriteInt32(integerTypes.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a NonNegativeForIntegerTypes");
        }
    }

    public Type ValueType => typeof(NotNegativeForIntegerTypes);
}