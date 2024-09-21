namespace Workout.V2.Application.BodyMuscles.V1.Commands.SoftDeleteBodyMuscle;

public record SoftDeleteBodyMuscleCommand(string Id) : IRequest;