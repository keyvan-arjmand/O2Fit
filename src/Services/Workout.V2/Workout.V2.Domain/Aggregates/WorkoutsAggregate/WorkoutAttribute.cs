namespace Workout.V2.Domain.Aggregates.WorkoutsAggregate;

public class WorkoutAttribute : BaseEntity
{
    public WorkoutAttribute()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public WorkoutAttribute(WorkoutAttributeTranslation name, List<WorkoutAttributeValue> workoutAttributeValues)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        WorkoutAttributeValues = workoutAttributeValues;
    }
    public WorkoutAttributeTranslation Name { get; set; }
    public List<WorkoutAttributeValue> WorkoutAttributeValues { get; set; }
}