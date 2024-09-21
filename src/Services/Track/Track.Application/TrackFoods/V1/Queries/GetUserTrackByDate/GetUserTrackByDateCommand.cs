using Track.Application.Dtos;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackByDate;

public record GetUserTrackByDateCommand(DateTime Date,string UserId):IRequest<List<UserTrackFoodDto>>;