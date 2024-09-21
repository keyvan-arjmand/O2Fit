namespace Workout.V2.Domain.Aggregates.BodyMusclesAggregate;

public class BodyMuscle : AggregateRoot
{
    public BodyMuscle()
    {
        
    }

    public BodyMuscle(BodyMuscleTranslation name)
    {
        Name = name;
    }
    public BodyMuscleTranslation Name { get; set; }
}