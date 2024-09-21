using Track.Application.Dtos;
using Track.Domain.Enums;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackByMeal;

public record GetUserTrackByMealCommand(string UserId, FoodMeal FoodMeal,DateTime Date) : IRequest<List<UserTrackFoodDto>>;