namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetAllNutritionistPendingOrders;

public record GetAllNutritionistPendingOrdersQuery(): IRequest<List<NutritionistOrderDto>>;