using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetAllRecipeSteps;

public record GetAllRecipeStepsQuery():IRequest<List<ListRecipeStepDto>>;