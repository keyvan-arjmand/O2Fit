namespace Nutritionist.Application.NutritionistOrders.V1.Commands.RejectNutritionistOrder;

public record RejectNutritionistOrderCommand(string Id) : IRequest;