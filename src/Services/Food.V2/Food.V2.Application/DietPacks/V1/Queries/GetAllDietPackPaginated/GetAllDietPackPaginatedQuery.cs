namespace Food.V2.Application.DietPacks.V1.Queries.GetAllDietPackPaginated;

public record GetAllDietPackPaginatedQuery(string? DietPackName, int DailyCalorie, FoodMeal? FoodMeal, string DietCategoryId, int PageIndex, int PageSize) 
                                            : IRequest<PaginationResult<GetAllDietPackDto>>;