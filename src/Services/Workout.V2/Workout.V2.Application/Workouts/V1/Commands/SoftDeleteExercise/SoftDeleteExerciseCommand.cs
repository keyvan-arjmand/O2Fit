namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteExercise;

public record SoftDeleteExerciseCommand(string Id) : IRequest;