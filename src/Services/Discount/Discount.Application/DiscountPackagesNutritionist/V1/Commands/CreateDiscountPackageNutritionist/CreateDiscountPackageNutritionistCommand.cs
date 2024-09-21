using Common.Enums.TypeEnums;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.CreateDiscountPackageNutritionist;

public class CreateDiscountPackageNutritionistCommand : IRequest<string>
{
    public string PackageId { get; set; } = string.Empty;
    public int Percent { get; set; }
}