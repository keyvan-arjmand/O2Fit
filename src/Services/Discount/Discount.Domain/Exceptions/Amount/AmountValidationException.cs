namespace Discount.Domain.Exceptions.Amount;

public class AmountValidationException : Exception
{
    public AmountValidationException(string message) : base(message)
    {

    }
}