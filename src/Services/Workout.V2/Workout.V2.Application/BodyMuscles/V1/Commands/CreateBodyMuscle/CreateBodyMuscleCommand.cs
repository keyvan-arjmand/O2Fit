namespace Workout.V2.Application.BodyMuscles.V1.Commands.CreateBodyMuscle;

public record CreateBodyMuscleCommand(CreateBodyMuscleTranslationDto Translation) : IRequest;