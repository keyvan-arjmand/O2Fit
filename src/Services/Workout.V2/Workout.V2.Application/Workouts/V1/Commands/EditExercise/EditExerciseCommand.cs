namespace Workout.V2.Application.Workouts.V1.Commands.EditExercise;

public record EditExerciseCommand(string Id, string Tag, double BurnedCalories, string Image, string Icon,
     CreateTranslationDto Name, List<CreateWorkoutAttributeDto>? Attributes) : IRequest;