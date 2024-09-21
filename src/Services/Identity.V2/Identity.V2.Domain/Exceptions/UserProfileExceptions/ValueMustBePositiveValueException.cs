namespace Identity.V2.Domain.Exceptions.UserProfileExceptions;

public class ValueMustBePositiveValueException : Exception
{
    public ValueMustBePositiveValueException(string message): base(message)
    {
        
    }
}