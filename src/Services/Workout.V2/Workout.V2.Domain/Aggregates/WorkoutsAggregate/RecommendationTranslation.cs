namespace Workout.V2.Domain.Aggregates.WorkoutsAggregate;

public class RecommendationTranslation : BaseEntity
{
    public RecommendationTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public RecommendationTranslation(string persian, string english, string arabic)
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