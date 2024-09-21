using Track.Application.Dtos;

namespace Track.Application.TrackWaters.V1.Queries.GetTrackWaterByDate;

public record GetTrackWaterByDateQuery(string UserId, DateTime? StartDate ,DateTime? EndDate):IRequest<List<UserTrackWaterDto>>;