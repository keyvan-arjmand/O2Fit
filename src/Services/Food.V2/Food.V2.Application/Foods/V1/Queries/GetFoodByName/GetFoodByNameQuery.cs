namespace Food.V2.Application.Foods.V1.Queries.GetFoodByName;

public record GetFoodByNameQuery
    (int Page, int PageSize, string FoodName, string Lang) : IRequest<
        PaginationResult<SearchFoodNameDto>>;