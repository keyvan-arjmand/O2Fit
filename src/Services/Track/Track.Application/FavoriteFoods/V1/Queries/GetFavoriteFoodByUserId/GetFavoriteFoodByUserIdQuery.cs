using Track.Application.Dtos;

namespace Track.Application.FavoriteFoods.V1.Queries.GetFavoriteFoodByUserId;

public record GetFavoriteFoodByUserIdQuery
    (string UserId, int Page, int PageSize, string Lang) : IRequest<List<FavoriteFoodGetDto>>;