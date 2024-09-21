using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.ProblemFood;

public class FaultFoodDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public TranslationDto Translation { get; set; } = new();
    public TranslationDto FoodTranslation { get; set; } = new();
    public string FoodId { get; set; } = string.Empty;
    public ReasonsProblem ReasonsProblem { get; set; }
    public DateTime Created { get; set; }
}