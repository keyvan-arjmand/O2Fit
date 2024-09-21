namespace Workout.V2.Application.BodyMuscles.V1.Commands.EditBodyMuscle;

public record EditBodyMuscleCommand(string Id, string Persian, string Arabic, string English) : IRequest;