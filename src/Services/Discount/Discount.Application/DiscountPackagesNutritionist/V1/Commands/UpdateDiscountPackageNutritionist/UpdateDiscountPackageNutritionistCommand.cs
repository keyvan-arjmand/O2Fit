namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.UpdateDiscountPackageNutritionist;

public class UpdateDiscountPackageNutritionistCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public int Percent { get; set; }
}