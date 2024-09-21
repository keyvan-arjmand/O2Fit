namespace Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingById;

public record GetBodyBuildingByIdQuery(string Id) : IRequest<BodyBuildingDto>;