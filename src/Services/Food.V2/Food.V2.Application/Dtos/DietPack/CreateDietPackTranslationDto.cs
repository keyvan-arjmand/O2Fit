namespace Food.V2.Application.Dtos.DietPack;

public class CreateDietPackTranslationDto : IDto
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}