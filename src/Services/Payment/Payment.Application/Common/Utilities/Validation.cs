using Common.Enums.TypeEnums;

namespace Payment.Application.Common.Utilities;

public class Validation
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

    public static bool ValidationPackageTypeAdmin(PackageType package)
    {
        // if (package == PackageType.NutritionistPack) return false;
        // else 
            return true;
    }
    public static bool ValidationPackageTypeNutritionist(PackageType package)
    {
        if (package == PackageType.CalorieCounting || package == PackageType.Diet) return false;
        else return true;
    }
}