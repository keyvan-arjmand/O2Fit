namespace Discount.Domain.Exceptions.Discount;

public class DiscountCodeTypeCannotBeNullOrEmptyException : Exception
{
    public DiscountCodeTypeCannotBeNullOrEmptyException(string message) : base(message) { }
}