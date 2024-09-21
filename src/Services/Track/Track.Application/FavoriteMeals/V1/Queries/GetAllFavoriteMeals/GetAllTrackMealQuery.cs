using Track.Application.Dtos;

namespace Track.Application.FavoriteMeals.V1.Queries.GetAllFavoriteMeals;

public class GetAllTrackMealQuery : IRequest<List<TrackMealDto>>
{
    public string UserId { get; set; } = string.Empty;
}