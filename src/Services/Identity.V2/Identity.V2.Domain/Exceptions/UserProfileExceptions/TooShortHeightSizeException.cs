namespace Identity.V2.Domain.Exceptions.UserProfileExceptions;

public class TooShortHeightSizeException: Exception
{
    public TooShortHeightSizeException(string message) : base(message)
    {
        
    }
}