namespace Identity.V2.Domain.ValueObjects;

[BsonSerializer(typeof(HeightSizeSerializer))]
public class HeightSize : ValueObject
{
    public int Value { get; private set; }

    public HeightSize()
    {
        
    }

    public HeightSize(int value)
    {
        if (value < 66)
            throw new TooShortHeightSizeException("this human is too short");
        if (value > 230)
            throw new TooLongHeightSizeException("this human is too long");
        Value = value;
    }
    public static implicit operator int(HeightSize heightSize) => heightSize.Value;
    public static explicit operator HeightSize(int value) => new(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}