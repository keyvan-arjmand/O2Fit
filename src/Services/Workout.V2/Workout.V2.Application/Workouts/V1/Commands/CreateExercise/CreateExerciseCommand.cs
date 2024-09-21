namespace Workout.V2.Application.Workouts.V1.Commands.CreateExercise;

public record CreateExerciseCommand(string Tag, double BurnedCalories,string Image, string Icon,
     CreateTranslationDto Name,  List<CreateWorkoutAttributeDto>? Attributes): IRequest;