namespace Workout.V2.Domain.Aggregates.WorkoutsAggregate;

public class Workout : AggregateRoot
{
    public Workout()
    {
        
    }

    public Workout(WorkoutNameTranslation name, RecommendationTranslation recommendation, HowToDoTranslation howToDo, string iconName, string imageName, string videoName, double burnedCalories, string tag, Classification classification, Gender gender, Level level, TargetMuscles targetMuscles, CardioCategory cardioCategory, List<ObjectId> bodyMuscleIds, List<WorkoutAttribute> workoutAttribute)
    {
        Name = name;
        Recommendation = recommendation;
        HowToDo = howToDo;
        IconName = iconName;
        ImageName = imageName;
        VideoName = videoName;
        BurnedCalories = burnedCalories;
        Tag = tag;
        Classification = classification;
        Gender = gender;
        Level = level;
        TargetMuscles = targetMuscles;
        CardioCategory = cardioCategory;
        BodyMuscleIds = bodyMuscleIds;
        WorkoutAttribute = workoutAttribute;
    }
    public WorkoutNameTranslation Name { get; set; }
    public RecommendationTranslation Recommendation { get; set; }
    public HowToDoTranslation HowToDo { get; set; }
    public string IconName { get; set; }
    public string ImageName { get; set; }
    public string VideoName { get; set; }
    public double BurnedCalories { get; set; }
    public string Tag { get; set; }
    public Classification Classification { get; set; }
    public Gender Gender { get; set; }
    public Level Level { get; set; }
    public TargetMuscles TargetMuscles { get; set; }
    public CardioCategory CardioCategory { get; set; }
    public List<ObjectId> BodyMuscleIds { get; set; }
    public List<WorkoutAttribute> WorkoutAttribute { get; set; }
    
}