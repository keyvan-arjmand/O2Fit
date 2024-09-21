namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.UpdateDiscountPackageO2Fit;

public class UpdateDiscountPackageO2FitCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public int Percent { get; set; }
}