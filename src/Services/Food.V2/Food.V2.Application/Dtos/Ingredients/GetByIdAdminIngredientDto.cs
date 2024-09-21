namespace Food.V2.Application.Dtos.Ingredients;

public class GetByIdAdminIngredientDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public IngredientTranslationDto Translation { get; set; } = null!;
    public string ThumbName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string TagEn { get; set; } = string.Empty;
    public string TagAr { get; set; } = string.Empty;
    public string DefaultMeasureUnitId { get; set; } = null!;
    public string NutrientValue { get; set; } = null!;
    public List<string> MeasureUnitIds { get; set; } = null!;
}