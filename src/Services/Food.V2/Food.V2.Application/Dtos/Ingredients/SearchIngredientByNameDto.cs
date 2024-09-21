namespace Food.V2.Application.Dtos.Ingredients;

public class SearchIngredientByNameDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    //public IngredientTranslationDto Translation { get; set; } = null!;
}