using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSleepAggregate;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Application.TrackSleeps.V1.Commands.UpdateTrackSleep;

public class UpdateTrackSleepCommandHandler : IRequestHandler<UpdateTrackSleepCommand, UserTrackSleepDto>
{
    private readonly IUnitOfWork _work;

    public UpdateTrackSleepCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackSleepDto> Handle(UpdateTrackSleepCommand request, CancellationToken cancellationToken)
    {
        var track = await _work.GenericRepository<TrackSleep>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (track == null) throw new NotFoundException($"track Not Found {request.Id}");
        track.InsertDate = request.EndDate;
        track.Rate = request.Rate;
        track.Duration = request.EndDate - request.StartDate;
        var bmr = Calculate.CalBmr(Convert.ToDouble(request.Weight), Convert.ToDouble(request.Height), request.Age,
            request.Gender, null);
        track.BurnedCalories = Convert.ToDecimal((bmr / 1440) * (request.EndDate - request.StartDate).TotalMinutes)
            .CreateNotNegativeForDecimalType();
        await _work.GenericRepository<TrackSleep>()
            .UpdateOneAsync(x => x.Id == request.Id, track,
                new Expression<Func<TrackSleep, object>>[]
                {
                    x => x.Duration,
                    x => x.InsertDate,
                    x => x.Rate,
                    x => x.BurnedCalories,
                }, null, cancellationToken);
        return track.ToDto<UserTrackSleepDto>();
    }
}