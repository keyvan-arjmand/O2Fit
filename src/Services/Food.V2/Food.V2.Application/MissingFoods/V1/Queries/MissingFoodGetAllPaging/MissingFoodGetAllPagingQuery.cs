using Food.V2.Application.Dtos.MissingFood;

namespace Food.V2.Application.MissingFoods.V1.Queries.MissingFoodGetAllPaging;

public record MissingFoodGetAllPagingQuery(int Page, int PageSize) : IRequest<PaginationResult<MissingFoodDto>>;