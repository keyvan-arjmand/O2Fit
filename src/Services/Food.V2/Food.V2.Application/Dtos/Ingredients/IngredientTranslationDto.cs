namespace Food.V2.Application.Dtos.Ingredients;

public class IngredientTranslationDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}