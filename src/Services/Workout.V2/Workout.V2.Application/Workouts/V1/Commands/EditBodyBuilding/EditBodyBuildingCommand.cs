namespace Workout.V2.Application.Workouts.V1.Commands.EditBodyBuilding;

public record EditBodyBuildingCommand(string Id, string Tag, double BurnedCalories, IFormFile Image, IFormFile Icon, IFormFile Video,
    Level Level, Gender Gender, TargetMuscles TargetMuscle, List<string> BodyMuscleIds,
    CreateTranslationDto Name, CreateRecommendationTranslationDto Recommendation, CreateHowToDoTranslationDto HowToDo) : IRequest;
