namespace Identity.V2.Domain.Exceptions.UserProfileExceptions;

public class ValueMustBePositiveValueForIntegerTypesException : Exception
{
    public ValueMustBePositiveValueForIntegerTypesException(string message): base(message)
    {
        
    }
}