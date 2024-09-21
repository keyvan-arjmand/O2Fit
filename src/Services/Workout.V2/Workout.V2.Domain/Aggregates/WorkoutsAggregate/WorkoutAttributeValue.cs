namespace Workout.V2.Domain.Aggregates.WorkoutsAggregate;

public class WorkoutAttributeValue : BaseEntity
{
    public WorkoutAttributeValue()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public WorkoutAttributeValue(WorkoutAttributeValueNameTranslation name, double burnedCalories)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        BurnedCalories = burnedCalories;
    }
    public WorkoutAttributeValueNameTranslation Name { get; set; }
    public double BurnedCalories { get; set; }
}