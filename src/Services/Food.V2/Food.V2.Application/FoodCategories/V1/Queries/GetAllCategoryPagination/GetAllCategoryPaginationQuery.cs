using Food.V2.Application.Dtos.FoodCategory;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetAllCategoryPagination;

public record GetAllCategoryPaginationQuery(int Page, int PageSize) : IRequest<List<FoodCategoryDto>>;