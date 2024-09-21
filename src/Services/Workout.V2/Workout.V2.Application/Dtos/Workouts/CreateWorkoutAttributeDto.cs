namespace Workout.V2.Application.Dtos.Workouts;

public class CreateWorkoutAttributeDto : IDto
{
    public CreateWorkoutAttributeDto()
    {
        
    }

    public CreateWorkoutAttributeDto(CreateTranslationDto name, List<CreateWorkoutAttributeValueDto> workoutAttributeValue)
    {
        Name = name;
        WorkoutAttributeValue = workoutAttributeValue;
    }
    public CreateTranslationDto Name { get; set; }
    public List<CreateWorkoutAttributeValueDto> WorkoutAttributeValue { get; set; }
}