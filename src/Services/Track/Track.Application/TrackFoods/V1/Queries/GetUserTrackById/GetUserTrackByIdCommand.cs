using Track.Application.Dtos;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackById;

public record GetUserTrackByIdCommand(string Id) : IRequest<UserTrackFoodDto>;