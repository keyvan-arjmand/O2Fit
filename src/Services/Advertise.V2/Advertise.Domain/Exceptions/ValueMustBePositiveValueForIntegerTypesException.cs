namespace Advertise.Domain.Exceptions;

public class ValueMustBePositiveValueForIntegerTypesException : Exception
{
    public ValueMustBePositiveValueForIntegerTypesException(string message): base(message)
    {
        
    }
}