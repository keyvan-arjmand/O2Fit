namespace Identity.V2.Domain.Exceptions.UserProfileExceptions;

public class WeightTimeRangeCannotBeNegativeException : Exception
{
    public WeightTimeRangeCannotBeNegativeException(string message): base(message)
    {
        
    }
}