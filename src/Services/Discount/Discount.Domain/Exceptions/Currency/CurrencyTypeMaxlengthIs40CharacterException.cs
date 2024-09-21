namespace Discount.Domain.Exceptions.Currency;

public class CurrencyTypeMaxlengthIs40CharacterException : Exception
{
    public CurrencyTypeMaxlengthIs40CharacterException(string message) : base(message)
    {

    }
}