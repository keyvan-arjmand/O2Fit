using Currency.Domain.ValueObjects;
using MongoDB.Bson.Serialization;

namespace Currency.Domain.Serializers;

public class CurrencyTypeSerializer : IBsonSerializer<CurrencyCode>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new CurrencyCode(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CurrencyCode value)
    {
        context.Writer.WriteString(value.Name);
    }

    public CurrencyCode Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new CurrencyCode(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is CurrencyCode currency)
        {
            context.Writer.WriteString(currency.Name);
        }
        else
        {
            throw new NotSupportedException("This is not a CurrencyType");
        }
    }

    public Type ValueType => typeof(CurrencyCode);
}