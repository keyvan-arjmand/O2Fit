using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;

public record GetByIdRecipeQuery(string Id,string FoodId):IRequest<RecipeStepDto>;