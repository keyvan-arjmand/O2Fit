namespace Workout.V2.Domain.Aggregates.BodyMusclesAggregate;

public class BodyMuscleTranslation : BaseEntity
{
    public BodyMuscleTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public BodyMuscleTranslation(string persian, string english, string arabic)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }
}