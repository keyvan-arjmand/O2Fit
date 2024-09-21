using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.Recipes.V1.Queries.GetByRecipeById;

public record GetByRecipeByIdQuery(string Id):IRequest<RecipeDto>;