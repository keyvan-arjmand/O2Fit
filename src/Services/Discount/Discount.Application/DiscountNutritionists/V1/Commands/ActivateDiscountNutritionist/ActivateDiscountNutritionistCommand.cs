namespace Discount.Application.DiscountNutritionists.V1.Commands.ActivateDiscountNutritionist;

public record ActivateDiscountNutritionistCommand(string Id, bool IsActive) : IRequest;