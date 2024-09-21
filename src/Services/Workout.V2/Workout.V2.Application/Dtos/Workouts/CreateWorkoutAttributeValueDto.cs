namespace Workout.V2.Application.Dtos.Workouts;

public class CreateWorkoutAttributeValueDto : IDto
{
    public CreateWorkoutAttributeValueDto()
    {
        
    }

    public CreateWorkoutAttributeValueDto(CreateTranslationDto name, double burnedCalories)
    {
        Name = name;
        BurnedCalories = burnedCalories;
    }
    public CreateTranslationDto Name { get; set; }
    public double BurnedCalories { get; set; }

}