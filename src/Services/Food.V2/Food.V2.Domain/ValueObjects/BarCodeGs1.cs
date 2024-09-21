namespace Food.V2.Domain.ValueObjects;

[BsonSerializer(typeof(BarCodeGs1Serializer))]
public class BarCodeGs1 : ValueObject
{
    public string Value { get; private set; }
    public BarCodeGs1()
    {
        
    }

    public BarCodeGs1(string value)
    {
        //if (string.IsNullOrEmpty(value))
        //    throw new BarCodeGs1CannotBeNullOrEmptyException("Bar code gs1 can not be null or empty");

        if (value.Length != 13)
            throw new BarCodeGs1LengthIs13Characters("Bar code gs1 length must be 13 characters");
            
        Value = value;
    }
    public static implicit operator string(BarCodeGs1 barCodeGs1) => barCodeGs1.Value;
    public static explicit operator BarCodeGs1(string value) => new(value);

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}