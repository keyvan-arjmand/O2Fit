namespace Nutritionist.Application.NutritionistUserPackages.V1.Commands.NutritionistAcceptOrder;

public record NutritionistAcceptOrderCommand(string OrderId, OrderStatus OrderStatus, string UserId) : IRequest;