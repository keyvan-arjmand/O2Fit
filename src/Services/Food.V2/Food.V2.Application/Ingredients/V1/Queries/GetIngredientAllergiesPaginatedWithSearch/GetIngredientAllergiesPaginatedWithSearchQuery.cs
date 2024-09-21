namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientAllergiesPaginatedWithSearch;

public record GetIngredientAllergiesPaginatedWithSearchQuery(int PageIndex, int PageSize, string Name) : IRequest<PaginationResult<IngredientDto>>;