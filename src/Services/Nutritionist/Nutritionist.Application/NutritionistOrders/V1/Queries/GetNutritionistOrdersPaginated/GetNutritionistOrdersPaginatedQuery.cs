namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetNutritionistOrdersPaginated;

public record GetNutritionistOrdersPaginatedQuery(Language Language, int PageNumber, int PageSize) : IRequest<PaginationResult<NutritionistOrderDto>>;