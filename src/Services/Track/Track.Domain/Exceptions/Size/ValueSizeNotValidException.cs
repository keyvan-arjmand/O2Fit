namespace Track.Domain.Exceptions.Size;

public class ValueSizeNotValidException : Exception
{
    public ValueSizeNotValidException(string message) : base(message)
    {

    }
}