using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetRecipeStepsByFoodId;

public record GetRecipeStepsByFoodIdQuery(string Id):IRequest<List<TranslationDto>>;