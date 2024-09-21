namespace Food.V2.Domain.Exceptions;

public class BarCodeNationalLengthIs16Characters : Exception
{
    public BarCodeNationalLengthIs16Characters(string message) : base(message)
    {
    }
}