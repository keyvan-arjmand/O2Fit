namespace Workout.V2.Application.BodyMuscles.V1.Queries.GetBodyMuscleById;

public record GetBodyMuscleByIdQuery(string Id) : IRequest<BodyMuscleDto>;
