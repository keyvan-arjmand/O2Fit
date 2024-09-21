namespace Discount.Application.DiscountNutritionists.V1.Commands.SubtractUsableDiscountNutritionist;

public class SubtractUsableDiscountNutritionistCommand:IRequest
{
    public string Code { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}