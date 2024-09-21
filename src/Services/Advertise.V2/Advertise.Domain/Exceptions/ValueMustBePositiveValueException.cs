namespace Advertise.Domain.Exceptions;

public class ValueMustBePositiveValueException : Exception
{
    public ValueMustBePositiveValueException(string message): base(message)
    {
        
    }
}