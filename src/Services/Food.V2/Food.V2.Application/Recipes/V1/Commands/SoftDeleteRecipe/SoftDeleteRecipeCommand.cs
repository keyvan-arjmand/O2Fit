namespace Food.V2.Application.Recipes.V1.Commands.SoftDeleteRecipe;

public record SoftDeleteRecipeCommand(string Id):IRequest;