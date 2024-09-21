namespace Food.V2.Application.Ingredients.V1.Queries.SearchIngredientByNamePaginated;

public record SearchIngredientByNamePaginatedQuery(string Name, int PageIndex, int PageSize, string Language) : IRequest<PaginationResult<SearchIngredientByNameDto>>;