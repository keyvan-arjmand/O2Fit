namespace Track.Application.FavoriteRecipes.V1.Commands.SoftDeleteFavoriteRecipe;

public record SoftDeleteFavoriteRecipeCommand(string FoodId):IRequest;