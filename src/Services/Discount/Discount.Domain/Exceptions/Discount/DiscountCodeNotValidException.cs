namespace Discount.Domain.Exceptions.Discount;

public class DiscountCodeNotValidException : Exception
{
    public DiscountCodeNotValidException(string message) : base(message)
    {

    }
}