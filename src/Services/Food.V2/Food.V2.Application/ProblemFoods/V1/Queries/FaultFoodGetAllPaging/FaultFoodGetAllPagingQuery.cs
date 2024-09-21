using Food.V2.Application.Dtos.ProblemFood;

namespace Food.V2.Application.ProblemFoods.V1.Queries.FaultFoodGetAllPaging;

public record FaultFoodGetAllPagingQuery(int Page, int PageSize) : IRequest<PaginationResult<FaultFoodDto>>;