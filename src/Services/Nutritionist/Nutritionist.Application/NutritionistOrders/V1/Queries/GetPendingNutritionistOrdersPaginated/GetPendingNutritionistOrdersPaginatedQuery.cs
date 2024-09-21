namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetPendingNutritionistOrdersPaginated;

public record GetPendingNutritionistOrdersPaginatedQuery(Language Language, int PageNumber, int PageSize) : IRequest<PaginationResult<NutritionistOrderDto>>;