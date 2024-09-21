namespace Food.V2.Application.Dtos.RecipeCategory;

[BsonIgnoreExtraElements]
public class RecipeCategoryTranslationDto
{
    public string Id { get; set; } = string.Empty;
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}