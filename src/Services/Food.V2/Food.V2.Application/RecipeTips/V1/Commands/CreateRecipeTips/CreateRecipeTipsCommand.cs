using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.RecipeTips.V1.Commands.CreateRecipeTips;

public class CreateRecipeTipsCommand : IRequest
{
    public string FoodId { get; set; } = string.Empty;
    public List<TranslationDto> Tips { get; set; } = default!;
}