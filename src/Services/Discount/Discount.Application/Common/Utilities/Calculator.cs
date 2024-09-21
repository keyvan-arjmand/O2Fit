namespace Discount.Application.Common.Utilities;

public static class Calculator
{
    //discount percent
    public static double CalculateWithPercent(this double price, int percent)
    {
        //کسر درصد از مبلغ
        return price - (price / 100 * percent);
    }
    public static double CalculateAmountPercent(this double price, int percent)
    {
        //مبلغ درصد
        return (price / 100) * percent;
    }
    //discount Amount
    public static double CalculateWithPercentAndAmount(this double price, int percent, double amount)
    {
        //کسر درصد و مبلغ
        return price - (price / 100 * percent) - amount;
    }
    public static double CalculateWithDiscountCode(this double price, double discount)
    {
        //کسر مبلغ
        return price - discount;
    }
    //exChange Currency 
    public static double CalculateWithPercent(int percent, double price)
    {
        var discount = ((price / 100) * percent);
        return price - discount;
    }
    public static double CalculateAmountWithPercent(int percent, double price)
    {
        return ((price / 100) * percent);
    }
    public static double CalculateWithPercentAndAmount(int percent, double price, double discount)
    {
        return (price - ((price / 100 * percent) - discount));
    }

    public static double CurrencyExchangeToDollar(this double amount, double exchangeRate)
    {
        return (amount / exchangeRate);
    }

    public static double CurrencyExchanger(this double amount, double exchangeRate, double exchangeRateTo)
    {
        return CurrencyExchangeToDollar(amount,exchangeRate) * exchangeRateTo;
    }
}