namespace Currency.Domain.Exceptions.Currency;

public class CurrencyTypeNotValidException : Exception
{
    public CurrencyTypeNotValidException(string message) : base(message)
    {

    }
}