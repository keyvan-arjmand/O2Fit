using Discount.Domain.Common;
using Discount.Domain.Exceptions;
using Discount.Domain.Serializers;
using System.Text.RegularExpressions;
using Discount.Domain.Exceptions.Discount;

namespace Discount.Domain.ValueObjects;
[BsonSerializer(typeof(DiscountCodeSerializer))]
public class DiscountCode : ValueObject
{
    public DiscountCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new DiscountCodeTypeCannotBeNullOrEmptyException("code can not be null or empty");
        // var regex = new Regex(@"^[A-Za-z\d_]+$");
        // if (!regex.IsMatch(code)) throw new DiscountCodeNotValidException("code Not Valid");
        if (code.Length < 4) throw new DiscountCodeMinLengthIs4CharacterException("code min length is 4 character");
        if (code.Length > 10) throw new DiscountCodeTypeMaxlengthIs10CharacterException("code max length is 10 character");
        Code = code;
    }

    public string Code { get; private set; }
    public static implicit operator string(DiscountCode name) => name.Code;
    public static explicit operator DiscountCode(string name) => new(name);

    public override string ToString()
    {
        return Code;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}