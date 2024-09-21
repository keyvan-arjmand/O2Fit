using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.RecipeSteps.V1.Commands.PatchRecipeStep;

public class PatchRecipeStepCommand : IRequest
{
    public string FoodId { get; set; } = string.Empty;
    public List<TranslationDto> Steps { get; set; } = new();
}