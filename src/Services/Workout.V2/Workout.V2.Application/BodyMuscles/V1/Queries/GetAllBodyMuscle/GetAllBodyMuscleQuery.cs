namespace Workout.V2.Application.BodyMuscles.V1.Queries.GetAllBodyMuscle;

public record GetAllBodyMuscleQuery() : IRequest<List<BodyMuscleDto>>;
