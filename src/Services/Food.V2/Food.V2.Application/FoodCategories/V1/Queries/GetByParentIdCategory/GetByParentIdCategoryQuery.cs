using Food.V2.Application.Dtos.FoodCategory;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetByParentIdCategory;

public record GetByParentIdCategoryQuery(string Id):IRequest<List<FoodCategoryDto>>;