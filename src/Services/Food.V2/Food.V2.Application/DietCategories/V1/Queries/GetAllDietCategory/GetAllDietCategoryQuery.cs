using Food.V2.Application.Dtos.DietCategory;

namespace Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategory;

public record GetAllDietCategoryQuery():IRequest<List<DietCategoryDto>>;