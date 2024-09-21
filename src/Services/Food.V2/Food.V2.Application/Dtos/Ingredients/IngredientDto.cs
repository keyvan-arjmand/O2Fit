namespace Food.V2.Application.Dtos.Ingredients;

public class IngredientDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string ThumbName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string? TagEn { get; set; } 
    public string? TagAr { get; set; }
    public IngredientTranslationDto Translation { get; set; } = new();
}