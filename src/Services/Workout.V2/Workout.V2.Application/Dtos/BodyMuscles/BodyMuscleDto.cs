namespace Workout.V2.Application.Dtos.BodyMuscles;

public class BodyMuscleDto : IDto
{
    public BodyMuscleDto()
    {
        
    }

    public BodyMuscleDto(string id, BodyMuscleTranslationDto name)
    {
        Id = id;
        Name = name;
    }
    public string Id { get; set; }
    public BodyMuscleTranslationDto Name { get; set; }
}