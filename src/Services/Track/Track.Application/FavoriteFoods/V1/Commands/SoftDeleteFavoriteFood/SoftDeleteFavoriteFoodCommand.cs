namespace Track.Application.FavoriteFoods.V1.Commands.SoftDeleteFavoriteFood;

public record SoftDeleteFavoriteFoodCommand(string Id):IRequest;