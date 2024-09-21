namespace Food.V2.Application.Dtos.Ingredients;

public class GetIngredientByIdDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public IngredientTranslationDto Name { get; set; } = null!;
    public string Code { get; set; } = string.Empty;
    public string ThumbName { get; set; } = string.Empty;
    public string NutrientValue { get; set; } = null!;
    public List<MeasureUnitDto> MeasureUnits { get; set; } = new();
}