using MongoDB.Bson.Serialization;
using Track.Domain.ValueObjects;

namespace Track.Domain.Serializers;

public class NotNegativeForDecimalTypesSerializer: IBsonSerializer<NotNegativeForDecimalTypes>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadDecimal128();
        return new NotNegativeForDecimalTypes((decimal)value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        NotNegativeForDecimalTypes value)
    {
        context.Writer.WriteDecimal128(Decimal128.ToDecimal(value.Value));
    }

    public NotNegativeForDecimalTypes Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDecimal128();
        return new NotNegativeForDecimalTypes((decimal)value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is NotNegativeForDecimalTypes decimalTypes)
        {
            context.Writer.WriteDecimal128(decimalTypes.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a NotNegativeForDecimalTypes");
        }
    }

    public Type ValueType => typeof(NotNegativeForDecimalTypes);
}