using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodByFilter;

public class GetFoodByFilterQuery : IRequest<List<FilterFoodDto>>
{
    public FoodSearchFilter Filter { get; set; } = default!;
}