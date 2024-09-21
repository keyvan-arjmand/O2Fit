using Common.Constants.Package;
using Common.Enums.TypeEnums;
using Payment.Application.Common.Exceptions;

namespace Payment.Application.Common.Utilities;

public static class Helper
{
    public static int GetDuration(this DurationPackage duration)
    {
        switch (duration)
        {
            case DurationPackage.DurationDiet:
                return DurationConstants.DurationDiet;
            case DurationPackage.DurationCalorieCounting6Month:
                return DurationConstants.DurationCalorieCounting6Month;
            case DurationPackage.DurationCalorieCountingMonthly:
                return DurationConstants.DurationCalorieCountingMonthly;
            case DurationPackage.DurationCalorieCountingYearly:
                return DurationConstants.DurationCalorieCountingYearly;
            case DurationPackage.DurationNutritionist:
                return DurationConstants.DurationNutritionist;
            default:
                throw new ArgumentOutOfRangeException(nameof(duration), duration, null);
        }
    }

    public static PaymentType GetPaymentType(this PackageType type)
    {
        if (type == PackageType.O2Package || type == PackageType.Diet || type == PackageType.CalorieCounting)
        {
            return PaymentType.PackageO2;
        }
        else
        {
            return PaymentType.PackageNutritionist;
        }
    }

    public static ObjectId StringToObjectId(this string id)
    {
        if (!string.IsNullOrEmpty(id) && !ObjectId.TryParse(id, out _))
            throw new AppException("Id Not Valid");
        return string.IsNullOrEmpty(id) ? ObjectId.Empty : ObjectId.Parse(id);
    }

    public static string ObjectIdToString(this ObjectId? id)
    {
        if (id.Equals(ObjectId.Empty))
        {
            return string.Empty;
        }
        else
        {
            return id.ToString();
        }
    }
    public static double ModNumber(this double budget, double cost)
    {
        return budget % cost;
    }
    public static long CountModNumber(this double budget, double cost)
    {
        if (budget.ModNumber(cost) != 0)
            throw new AppException("number not a divisible");
        return (long)(budget / cost);
    }
}