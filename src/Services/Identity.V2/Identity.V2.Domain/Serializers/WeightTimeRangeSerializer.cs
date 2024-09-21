namespace Identity.V2.Domain.Serializers;

public class WeightTimeRangeSerializer : IBsonSerializer<WeightTimeRange>
{
    object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }
        var value = context.Reader.ReadInt32();
        return new WeightTimeRange(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, WeightTimeRange value)
    {
        context.Writer.WriteInt32(value.Value);
    }

    public WeightTimeRange Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new WeightTimeRange(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object? value)
    {
        if (value is null)
        {
            context.Writer.WriteNull();
        }
        else if (value is WeightTimeRange weightTimeRange)
        {
            context.Writer.WriteInt32(weightTimeRange.Value);
        }
        else
        {
            throw new NotSupportedException("This is not a WeightTimeRange");
        }
    }

    public Type ValueType => typeof(WeightTimeRange);
}