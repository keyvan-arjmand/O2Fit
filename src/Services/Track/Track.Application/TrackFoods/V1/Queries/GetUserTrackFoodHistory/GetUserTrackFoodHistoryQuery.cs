using Track.Application.Dtos;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackFoodHistory;

public record GetUserTrackFoodHistoryQuery(string UserId, DateTime DateTime) : IRequest<List<UserTrackFoodDto>>;