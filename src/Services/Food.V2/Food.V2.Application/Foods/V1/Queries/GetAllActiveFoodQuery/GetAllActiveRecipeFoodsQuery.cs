using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.Foods.V1.Queries.GetAllActiveFoodQuery;

public record GetAllActiveRecipeFoodsQuery():IRequest<List<FullRecipeDto>>;