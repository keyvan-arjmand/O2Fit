namespace Discount.Domain.Exceptions.Discount;

public class DiscountCodeMinLengthIs4CharacterException : Exception
{
    public DiscountCodeMinLengthIs4CharacterException(string message) : base(message)
    {

    }
}