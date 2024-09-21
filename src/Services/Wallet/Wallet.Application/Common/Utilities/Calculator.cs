namespace Wallet.Application.Common.Utilities;

public static class Calculator
{
    public static Commission Commission(this double amount)
    {
        return new Commission
        {
            Amount = amount,
            NetIncome = amount.NetIncome(),
            Income = amount.Income(),
            ValueAdded = amount.ValueAdded(),
            DebtToTheNutritionist = amount.DebtToTheNutritionist(),
        };
    }

    public static double NetIncome(this double amount)
    {
        return PercentageCalculation(amount, 15);
    }

    public static double NetIncomeWithDiscount(this double amount, double discountAmount)
    {
        var commission = PercentageCalculation(amount, 15);
        var tax = PercentageCalculation(commission, 9);
        return commission - tax - discountAmount;
    }

    public static double Income(this double amount)
    {
        var commission = PercentageCalculation(amount, 15);
        var tax = PercentageCalculation(commission, 9);
        return commission + tax;
    }

    public static double IncomeWithDiscount(this double amount, double discountAmount)
    {
        var commission = PercentageCalculation(amount, 15);
        var tax = PercentageCalculation(commission, 9);
        return commission + tax - discountAmount;
    }

    public static double ValueAdded(this double amount)
    {
        var commission = PercentageCalculation(amount, 15);
        return PercentageCalculation(commission, 9);
    }

    public static double DebtToTheNutritionistWithDiscount(this double amount, double discountAmount)
    {
        return amount - amount.IncomeWithDiscount(discountAmount);
    }

    public static double DebtToTheNutritionist(this double amount)
    {
        return amount - amount.Income();
    }

    public static double PercentageCalculation(this double amount, int percent)
    {
        return (amount / 100) * percent;
    }
}

public class Commission
{
    public double Amount { get; set; }
    public double NetIncome { get; set; }
    public double Income { get; set; }
    public double ValueAdded { get; set; }
    public double DebtToTheNutritionist { get; set; }
}