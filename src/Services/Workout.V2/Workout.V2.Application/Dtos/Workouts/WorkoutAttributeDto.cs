namespace Workout.V2.Application.Dtos.Workouts;

public class WorkoutAttributeDto : IDto
{
    public WorkoutAttributeDto()
    {
        
    }

    public WorkoutAttributeDto(CreateTranslationDto name, List<CreateWorkoutAttributeValueDto> workoutAttributeValues)
    {
        Name = name;
        WorkoutAttributeValues = workoutAttributeValues;
    }
    public CreateTranslationDto Name { get; set; }
    public List<CreateWorkoutAttributeValueDto> WorkoutAttributeValues { get; set; }
}