namespace Food.V2.Application.Dtos.Food;

public class CreateFoodTranslationDto : IDto
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}