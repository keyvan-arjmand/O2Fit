namespace Workout.V2.Application.Dtos.BodyMuscles;

public class CreateBodyMuscleTranslationDto : IDto
{
    public CreateBodyMuscleTranslationDto()
    {
        
    }

    public CreateBodyMuscleTranslationDto(string persian, string english, string arabic)
    {
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }    
}