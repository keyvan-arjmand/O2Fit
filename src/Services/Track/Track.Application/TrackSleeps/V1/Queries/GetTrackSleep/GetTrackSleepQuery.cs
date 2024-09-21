using Track.Application.Dtos;

namespace Track.Application.TrackSleeps.V1.Queries.GetTrackSleep;

public record GetTrackSleepQuery(string UserId, int Days):IRequest<UserTrackSleepAverageDto>;