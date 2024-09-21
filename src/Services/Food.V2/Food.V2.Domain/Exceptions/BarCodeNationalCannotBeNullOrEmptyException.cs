namespace Food.V2.Domain.Exceptions;

public class BarCodeNationalCannotBeNullOrEmptyException : Exception
{
    public BarCodeNationalCannotBeNullOrEmptyException(string message): base(message)
    {
    }
}