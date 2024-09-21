namespace Payment.Application.Common.Utilities;

public class Calculator
{
    public static double CalculateWithPercent(int percent, double price)
    {
        var discount = price / 100 * percent;
        return price - discount;
    }
    public static double CalculateAmountWithPercent(int percent, double price)
    {
       return price / 100 * percent;
    }
    public static double CalculateWithPercentAndAmount(int percent, double price, double discount)
    {
        return price - price / 100 * percent - discount;
    }
}