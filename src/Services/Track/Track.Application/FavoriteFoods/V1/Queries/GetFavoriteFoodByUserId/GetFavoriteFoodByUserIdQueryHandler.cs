using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteFoodAggregate;

namespace Track.Application.FavoriteFoods.V1.Queries.GetFavoriteFoodByUserId;

public class
    GetFavoriteFoodByUserIdQueryHandler : IRequestHandler<GetFavoriteFoodByUserIdQuery, List<FavoriteFoodGetDto>>
{
    private readonly IUnitOfWork _work;

    public GetFavoriteFoodByUserIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<FavoriteFoodGetDto>> Handle(GetFavoriteFoodByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteFood>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
        var result = await _work.GenericRepository<FavoriteFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.Select(x => new FavoriteFoodDto
        {
            AppId = x.AppId,
            UserId = x.CreatedById.ToString()!,
            FoodId = x.FoodId,
            Lang = request.Lang,
        }).ToList().MapTo<List<FavoriteFoodGetDto>, List<FavoriteFoodDto>>();
    }
}