namespace Discount.Domain.Exceptions.Discount;

public class DiscountCodeTypeMaxlengthIs10CharacterException : Exception
{
    public DiscountCodeTypeMaxlengthIs10CharacterException(string message) : base(message)
    {

    }
}