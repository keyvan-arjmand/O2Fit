using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Application.TrackWaters.V1.Queries.GetTrackWaterByDate;

public class GetTrackWaterByDateQueryHandler : IRequestHandler<GetTrackWaterByDateQuery, List<UserTrackWaterDto>>
{
    private readonly IUnitOfWork _work;

    public GetTrackWaterByDateQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackWaterDto>> Handle(GetTrackWaterByDateQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TrackWater>.Filter.Empty;
        var date = DateTime.Now.Date;
        switch (request.StartDate)
        {
            case null when request.EndDate == null:
                filter &= Builders<TrackWater>.Filter.Where(x =>
                    x.InsertDate == date && x.CreatedById == request.UserId.StringToInt());
                var firstCaseResult = await _work.GenericRepository<TrackWater>()
                    .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
                return firstCaseResult.ToDto<UserTrackWaterDto>().ToList();
            case null:
                filter = Builders<TrackWater>.Filter.Where(x =>
                    x.InsertDate == date && x.CreatedById == request.UserId.StringToInt());
                var result = await _work.GenericRepository<TrackWater>()
                    .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
                return result.ToDto<UserTrackWaterDto>().ToList();
            default:
                List<TrackWater> defaultResult = new List<TrackWater>();
                if (request.EndDate == null)
                {
                    filter = Builders<TrackWater>.Filter.Where(x =>
                        request.StartDate <= x.InsertDate.Date &&
                        x.InsertDate.Date <= date
                        && x.CreatedById == request.UserId.StringToInt());
                    defaultResult = await _work.GenericRepository<TrackWater>()
                        .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
                    return defaultResult.ToDto<UserTrackWaterDto>().ToList();
                }
                else
                {
                    filter = Builders<TrackWater>.Filter.Where(x =>
                        request.StartDate <= x.InsertDate.Date &&
                        x.InsertDate.Date <= request.EndDate &&
                        x.CreatedById == request.UserId.StringToInt());
                    defaultResult = await _work.GenericRepository<TrackWater>()
                        .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
                    return defaultResult.ToDto<UserTrackWaterDto>().ToList();
                }
        }
    }
}