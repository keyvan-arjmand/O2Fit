using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSleepAggregate;

namespace Track.Application.TrackSleeps.V1.Queries.GetByDateTrackSleep;

public class GetByDateTrackSleepQueryHandler : IRequestHandler<GetByDateTrackSleepQuery, UserTrackSleepDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByDateTrackSleepQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserTrackSleepDto> Handle(GetByDateTrackSleepQuery request, CancellationToken cancellationToken)
    {
        var userObjId = request.UserId.StringToInt();
        var filter =
            Builders<TrackSleep>.Filter.Where(x => x.InsertDate.Date == request.DateTime && x.CreatedById == userObjId);
        var result = await _unitOfWork.GenericRepository<TrackSleep>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        return result.ToDto<UserTrackSleepDto>();
    }
}