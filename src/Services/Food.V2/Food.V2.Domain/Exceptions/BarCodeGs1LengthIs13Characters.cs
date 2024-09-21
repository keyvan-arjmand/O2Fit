namespace Food.V2.Domain.Exceptions;

public class BarCodeGs1LengthIs13Characters : Exception
{
    public BarCodeGs1LengthIs13Characters(string message) : base(message)
    {
    }
}