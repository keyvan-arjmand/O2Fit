namespace Food.V2.Domain.ValueObjects;

[BsonSerializer(typeof(BarCodeNationalSerializer))]
public class BarCodeNational : ValueObject
{
    public string Value { get; private set; }
    public BarCodeNational()
    {
        
    }

    public BarCodeNational(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new BarCodeNationalCannotBeNullOrEmptyException("BarCodeNational can not be null or empty");

        if (value.Length != 16)
            throw new BarCodeNationalLengthIs16Characters("BarCodeNational length must be 13 characters");
            
        Value = value;
    }
    public static implicit operator string(BarCodeNational barCodeNational) => barCodeNational.Value;
    public static explicit operator BarCodeNational(string value) => new(value);

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}