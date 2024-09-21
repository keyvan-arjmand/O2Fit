using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Queries.GetAllPagingRecipe;

public record GetAllPagingRecipeQuery(int Page, int PageSize) : IRequest<PaginationResult<RecipeDto>>;