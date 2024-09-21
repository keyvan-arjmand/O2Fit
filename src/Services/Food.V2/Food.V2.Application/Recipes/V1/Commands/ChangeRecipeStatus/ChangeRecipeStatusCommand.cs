using Food.V2.Domain.Enums;

namespace Food.V2.Application.Recipes.V1.Commands.ChangeRecipeStatus;

public class ChangeRecipeStatusCommand:IRequest
{
    public string FoodId { get; set; } = string.Empty;
    public RecipeStatus Status { get; set; }
}