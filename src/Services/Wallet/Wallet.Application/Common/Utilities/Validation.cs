using Common.Enums.TypeEnums;

namespace Wallet.Application.Common.Utilities;

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
    public static bool ValidationDiscountPackage(this string packageType, string discountPackageType)
    {
        if (packageType.Equals(PackageType.CalorieCounting.ToDescription()) || packageType.Equals(PackageType.Diet.ToDescription()))
        {
            if (discountPackageType.Equals(PackageType.O2Package.ToDescription()) ||
                discountPackageType.Equals(PackageType.CalorieCounting.ToDescription()) ||
                discountPackageType.Equals(PackageType.Diet.ToDescription()))
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
            if (discountPackageType.Equals(PackageType.O2Package.ToDescription()) ||
                discountPackageType.Equals(PackageType.CalorieCounting.ToDescription()) ||
                discountPackageType.Equals(PackageType.Diet.ToDescription()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public static bool ValidO2Package(this PackageType packageType)
    {
        if (packageType == PackageType.Diet || packageType == PackageType.CalorieCounting ||
            packageType == PackageType.O2Package)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}