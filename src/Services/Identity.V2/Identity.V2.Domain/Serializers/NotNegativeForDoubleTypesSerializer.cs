namespace Identity.V2.Domain.Serializers;

public class NotNegativeForDoubleTypesSerializer: IBsonSerializer<NotNegativeForDoubleTypes>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadDouble();
        return new NotNegativeForDoubleTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        NotNegativeForDoubleTypes value)
    {
        context.Writer.WriteDouble(value);
    }

    public NotNegativeForDoubleTypes Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDouble();
        return new NotNegativeForDoubleTypes(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is NotNegativeForDoubleTypes doubleTypes)
        {
            context.Writer.WriteDouble(doubleTypes.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a NotNegativeForDoubleTypes");
        }
    }

    public Type ValueType => typeof(NotNegativeForDoubleTypes);
}