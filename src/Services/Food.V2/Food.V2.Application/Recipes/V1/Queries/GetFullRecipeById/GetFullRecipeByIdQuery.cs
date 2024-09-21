using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.Recipes.V1.Queries.GetFullRecipeById;

public record GetFullRecipeByIdQuery(string Id):IRequest<FullRecipeDto>;