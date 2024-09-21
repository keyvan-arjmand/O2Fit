using Track.Application.Dtos;

namespace Track.Application.TrackWaters.V1.Queries.GetUserTrackWaterHistory;

public record GetUserTrackWaterHistoryQuery(string UserId, DateTime DateTime) : IRequest<List<UserTrackWaterDto>>;