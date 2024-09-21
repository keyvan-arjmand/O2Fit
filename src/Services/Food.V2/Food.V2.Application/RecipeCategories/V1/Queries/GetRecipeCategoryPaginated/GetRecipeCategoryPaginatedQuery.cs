namespace Food.V2.Application.RecipeCategories.V1.Queries.GetRecipeCategoryPaginated;

public record GetRecipeCategoryPaginatedQuery(int PageIndex ,int PageSize) : IRequest<PaginationResult<RecipeCategoryDto>>;