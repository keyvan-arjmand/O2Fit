using Track.Application.Dtos;

namespace Track.Application.FavoriteRecipes.V1.Queries.GetAllFavoriteRecipe;

public record GetAllFavoriteRecipeQuery(string UserId):IRequest<List<TrackRecipeDto>>;