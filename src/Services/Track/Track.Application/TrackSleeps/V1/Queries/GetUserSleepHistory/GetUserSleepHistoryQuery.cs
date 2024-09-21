using Track.Application.Dtos;

namespace Track.Application.TrackSleeps.V1.Queries.GetUserSleepHistory;

public record GetUserSleepHistoryQuery(string UserId, DateTime DateTime) : IRequest<List<UserTrackSleepDto>>;