namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetAllCategoryPermissionsPaginated;

public record GetAllPermissionCategoriesPaginatedQuery(int PageSize, int PageIndex) : IRequest<PaginationResult<CategoryPermissionPaginatedDto>>;