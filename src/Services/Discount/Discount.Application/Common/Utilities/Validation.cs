using Common.Enums.TypeEnums;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;

namespace Discount.Application.Common.Utilities;

public static class Validation
{
    public static bool ValidationDate(DateTime timeStart, DateTime timeEnd)
    {
        switch (DateTime.Compare(timeStart, timeEnd))
        {
            case < 0:
                return true;
            case 0:
                return false;
            case > 0:
                return false;
        }
    }

    public static bool ValidationInvitationDiscountType(this DiscountType type)
    {
        if (type == DiscountType.InvitedDiscountCode || type == DiscountType.InviterDiscountCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidationDiscountType(this string type)
    {
        if (type.Equals(DiscountType.DiscountCodeGeneral) || type.Equals(DiscountType.DiscountCodePerUser))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidationDiscountPackage(this PackageType packageType, string discountPackageType)
    {
        if (packageType.Equals(PackageType.CalorieCounting) || packageType.Equals(PackageType.Diet))
        {
            if (discountPackageType.Equals(PackageType.O2Package) ||
                discountPackageType.Equals(PackageType.CalorieCounting) ||
                discountPackageType.Equals(PackageType.Diet))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (discountPackageType.Equals(PackageType.O2Package) ||
                discountPackageType.Equals(PackageType.CalorieCounting) ||
                discountPackageType.Equals(PackageType.Diet))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}