using Food.V2.Application.Dtos.FoodCategory;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetAllCategory;

public record GetAllCategoryQuery():IRequest<List<FoodCategoryDto>>;