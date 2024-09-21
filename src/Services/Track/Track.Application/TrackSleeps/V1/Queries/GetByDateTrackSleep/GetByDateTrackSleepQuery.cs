using Track.Application.Dtos;

namespace Track.Application.TrackSleeps.V1.Queries.GetByDateTrackSleep;

public record GetByDateTrackSleepQuery(string UserId, DateTime DateTime) : IRequest<UserTrackSleepDto>;