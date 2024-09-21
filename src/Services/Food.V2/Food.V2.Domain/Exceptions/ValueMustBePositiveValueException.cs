namespace Food.V2.Domain.Exceptions;

public class ValueMustBePositiveValueException : Exception
{
    public ValueMustBePositiveValueException(string message): base(message)
    {
        
    }
}