namespace Food.V2.Application.Dtos.RecipeCategory;


[BsonIgnoreExtraElements]
public class RecipeCategoryDto: IDto
{
    public string Id { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public RecipeCategoryTranslationDto Translation { get; set; } = null!;
}