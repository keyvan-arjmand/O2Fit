using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSleepAggregate;

namespace Track.Application.TrackSleeps.V1.Queries.GetUserSleepHistory;

public class GetUserSleepHistoryQueryHandler : IRequestHandler<GetUserSleepHistoryQuery, List<UserTrackSleepDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserSleepHistoryQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackSleepDto>> Handle(GetUserSleepHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TrackSleep>.Filter.Where(x => x.InsertDate.Date >= request.DateTime && x.CreatedById ==
    request.UserId.StringToInt());
        var result = await _work.GenericRepository<TrackSleep>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<UserTrackSleepDto>().ToList();
    }
}