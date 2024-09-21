using Food.V2.Application.Dtos.DietCategory;

namespace Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategoryPagination;

public record GetAllDietCategoryPaginationQuery(int Page ,int PageSize):IRequest<List<DietCategoryDto>>;