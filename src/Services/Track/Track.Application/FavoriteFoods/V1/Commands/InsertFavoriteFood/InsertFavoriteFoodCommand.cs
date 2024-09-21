using Track.Application.Dtos;

namespace Track.Application.FavoriteFoods.V1.Commands.InsertFavoriteFood;

public record InsertFavoriteFoodCommand(string FoodId, string AppId, string Lang) : IRequest<FavoriteFoodGetDto>;