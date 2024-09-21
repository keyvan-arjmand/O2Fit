namespace Food.V2.Domain.Exceptions;

public class BarCodeGs1CannotBeNullOrEmptyException : Exception
{
    public BarCodeGs1CannotBeNullOrEmptyException(string message) : base(message)
    {
        
    }
}