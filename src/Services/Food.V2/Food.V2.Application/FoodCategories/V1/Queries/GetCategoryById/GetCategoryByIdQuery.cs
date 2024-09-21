using Food.V2.Application.Dtos.FoodCategory;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetCategoryById;

public record GetCategoryByIdQuery(string Id) : IRequest<FoodCategoryDto>;