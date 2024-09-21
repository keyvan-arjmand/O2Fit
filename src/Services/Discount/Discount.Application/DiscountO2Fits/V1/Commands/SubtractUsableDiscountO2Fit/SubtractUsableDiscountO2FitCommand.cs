namespace Discount.Application.DiscountO2Fits.V1.Commands.SubtractUsableDiscountO2Fit;

public class SubtractUsableDiscountO2FitCommand:IRequest
{
    public string Code { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}