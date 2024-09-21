namespace Workout.V2.Application.Dtos.Workouts;

public class ExerciseDto : IDto
{
    public ExerciseDto()
    {
        
    }
    
    public string Id { get; set; }
    public WorkoutNameTranslationDto Name { get; set; }
    public string IconUrl { get; set; }
    public string ImageUrl { get; set; }
    //public string VideoName { get; set; }
    public double BurnedCalories { get; set; }
    public string Tag { get; set; }

    public List<WorkoutAttributeDto> WorkoutAttribute { get; set; }
    //public Classification Classification { get; set; }
    //public Gender Gender { get; set; }
    //public Level Level { get; set; }
    //public TargetMuscles TargetMuscles { get; set; }
    //public CardioCategory CardioCategory { get; set; }
}