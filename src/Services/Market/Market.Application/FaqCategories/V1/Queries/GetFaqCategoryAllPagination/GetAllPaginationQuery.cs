
using Market.Application.Dtos;

namespace Market.Application.FaqCategories.V1.Queries.GetFaqCategoryAllPagination;

public record GetAllPaginationQuery(int Page,int PageSize):IRequest<PaginationResult<FaqCategoryDto>>;