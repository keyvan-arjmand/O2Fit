namespace Workout.V2.Application.Dtos.Workouts;

public class RecommendationTranslationDto : IDto
{
    public RecommendationTranslationDto()
    {
        
    }

    public RecommendationTranslationDto(string persian, string english, string arabic)
    {
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }
}