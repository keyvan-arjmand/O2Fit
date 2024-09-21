namespace Track.Domain.Exceptions;

public class ValueMustBePositiveForDecimalTypesValueException : Exception
{
    public ValueMustBePositiveForDecimalTypesValueException(string message) : base(message)
    {
    }
}