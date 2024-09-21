using Track.Application.Dtos;

namespace Track.Application.FavoriteRecipes.V1.Queries.GetByFoodIdFavoriteRecipe;

public class GetByFoodIdFavoriteRecipeQuery : IRequest<TrackRecipeDto>
{
    public string FoodId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}