using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;

namespace Food.V2.Application.DietCategories.V1.Queries.GetCategoryById;

public record GetDietCategoryByIdQuery(string Id) : IRequest<DietCategoryDto>;