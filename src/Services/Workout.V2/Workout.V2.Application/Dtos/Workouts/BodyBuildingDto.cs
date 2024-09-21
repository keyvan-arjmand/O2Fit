namespace Workout.V2.Application.Dtos.Workouts;

public class BodyBuildingDto : IDto
{
    public string Id { get; set; }
    public WorkoutNameTranslationDto Name { get; set; }
    public string IconUrl { get; set; }
    public string ImageUrl { get; set; }
    public string VideoUrl { get; set; }
    public double BurnedCalories { get; set; }
    public string Tag { get; set; }
    public Gender Gender { get; set; }
    public Level Level { get; set; }
    public TargetMuscles TargetMuscles { get; set; }
    public RecommendationTranslationDto Recommendation { get; set; }
    public HowToDoTranslationDto HowToDo { get; set; }
    public List<BodyMuscleTranslationDto> BodyMuscles { get; set; } = new();
}