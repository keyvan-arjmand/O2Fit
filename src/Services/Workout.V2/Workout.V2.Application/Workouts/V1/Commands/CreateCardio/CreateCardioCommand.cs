namespace Workout.V2.Application.Workouts.V1.Commands.CreateCardio;

public record CreateCardioCommand(string Tag, double BurnedCalories, IFormFile Image, IFormFile Icon, IFormFile Video,
    Level Level, Gender Gender, CardioCategory CardioCategory,
    CreateTranslationDto Name) : IRequest;