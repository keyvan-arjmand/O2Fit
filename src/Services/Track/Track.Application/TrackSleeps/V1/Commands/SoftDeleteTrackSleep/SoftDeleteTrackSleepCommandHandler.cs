using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.TrackSleepAggregate;

namespace Track.Application.TrackSleeps.V1.Commands.SoftDeleteTrackSleep;

public class SoftDeleteTrackSleepCommandHandler : IRequestHandler<SoftDeleteTrackSleepCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteTrackSleepCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteTrackSleepCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<TrackSleep>.Filter.Eq(x => x.Id, request.Id);
        var track = await _work.GenericRepository<TrackSleep>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (track == null) throw new NotFoundException("Track not found");
        track.IsDelete = true;
        await _work.GenericRepository<TrackSleep>().SoftDeleteByIdAsync(track.Id, track, null, cancellationToken);
    }
}