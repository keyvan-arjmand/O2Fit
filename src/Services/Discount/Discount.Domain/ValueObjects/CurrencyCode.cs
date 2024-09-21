using System.Text.RegularExpressions;
using Discount.Domain.Common;
using Discount.Domain.Exceptions.Currency;
using Discount.Domain.Serializers;

namespace Discount.Domain.ValueObjects;

[BsonSerializer(typeof(CurrencyTypeSerializer))]
public class CurrencyCode : ValueObject
{
    public CurrencyCode()
    {
    }

    public CurrencyCode(string currencyType)
    {
        if (string.IsNullOrEmpty(currencyType))
            throw new CurrencyTypeCannotBeNullOrEmptyException("currencyType can not be empty");
        if (currencyType.Length != 3)
            throw new CurrencyCodeLenghtException("currencyCode Must To 3 character");
        var regex = new Regex(@"^[A-Za-z]+$");
        if (!regex.IsMatch(currencyType)) throw new CurrencyTypeNotValidException("currencyType Not Valid");
        Name = currencyType;
    }

    public string Name { get; private set; }
    public static implicit operator string(CurrencyCode Name) => Name.Name;
    public static explicit operator CurrencyCode(string name) => new(name);

    public override string ToString()
    {
        return Name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}