namespace Currency.Domain.Exceptions.Currency;

public class CurrencyTypeCannotBeNullOrEmptyException : Exception
{
    public CurrencyTypeCannotBeNullOrEmptyException(string message) : base(message)
    {

    }
}