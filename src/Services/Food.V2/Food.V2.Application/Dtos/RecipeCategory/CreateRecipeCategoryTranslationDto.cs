namespace Food.V2.Application.Dtos.RecipeCategory;

public class CreateUpdateRecipeCategoryTranslationDto : IDto
{
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}