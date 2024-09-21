namespace Workout.V2.Application.Workouts.V1.Queries.GetExerciseById;

public record GetExerciseByIdQuery(string Id) : IRequest<ExerciseDto>;