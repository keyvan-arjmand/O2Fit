namespace Food.V2.Domain.Serializers;

public class NotNegativeForIntegerTypesSerializer: IBsonSerializer<NonNegativeForIntegerTypes>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {  
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadInt32();
        return new NonNegativeForIntegerTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        NonNegativeForIntegerTypes value)
    {
        context.Writer.WriteInt32(value);
    }

    public NonNegativeForIntegerTypes Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new NonNegativeForIntegerTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is NonNegativeForIntegerTypes integerTypes)
        {
            context.Writer.WriteInt32(integerTypes.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a NonNegativeForIntegerTypes");
        }
    }

    public Type ValueType => typeof(NonNegativeForIntegerTypes);
}