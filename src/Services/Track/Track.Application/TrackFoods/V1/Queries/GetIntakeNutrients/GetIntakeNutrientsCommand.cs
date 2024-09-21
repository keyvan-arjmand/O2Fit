using Track.Application.Dtos;

namespace Track.Application.TrackFoods.V1.Queries.GetIntakeNutrients;

public record GetIntakeNutrientsCommand(string UserId,DateTime DateTime):IRequest<UserTrackNutrientDto>;