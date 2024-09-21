using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSleepAggregate;

namespace Track.Application.TrackSleeps.V1.Commands.InsertTrackSleep;

public class InsertTrackSleepCommandHandler : IRequestHandler<InsertTrackSleepCommand, UserTrackSleepDto>
{
    private readonly IUnitOfWork _work;

    public InsertTrackSleepCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackSleepDto> Handle(InsertTrackSleepCommand request, CancellationToken cancellationToken)
    {
        var bmr = Calculate.CalBmr(Convert.ToDouble(request.Weight), Convert.ToDouble(request.Height), request.Age,
            request.Gender, null);
        var track = new TrackSleep
        {
            InsertDate = request.EndDate,
            Duration = request.EndDate - request.StartDate,
            BurnedCalories = Convert.ToDecimal((bmr / 1440) * (request.EndDate - request.StartDate).TotalMinutes)
                .CreateNotNegativeForDecimalType(),
            AppId = request.AppId,
            Rate = request.Rate,
        };
        await _work.GenericRepository<TrackSleep>().InsertOneAsync(track, null, cancellationToken);
        return track.ToDto<UserTrackSleepDto>();
    }
}