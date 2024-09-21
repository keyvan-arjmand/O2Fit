using Track.Application.Dtos;

namespace Track.Application.FavoriteRecipes.V1.Commands.InsertTrackRecipe;

public class InsertFavoriteRecipeCommand:IRequest<TrackRecipeDto>
{
    public string UserId { get; set; } = string.Empty;
    public string FoodId { get; set; } = string.Empty;
}