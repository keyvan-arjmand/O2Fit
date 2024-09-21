using Common.Enums.TypeEnums;

namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.CreateDiscountPackageO2Fit;

public class CreateDiscountPackageO2FitCommand:IRequest<string>
{
    public string PackageId { get; set; } = string.Empty;
    public int Percent { get; set; }
    public PackageType PackageType { get; set; }
}