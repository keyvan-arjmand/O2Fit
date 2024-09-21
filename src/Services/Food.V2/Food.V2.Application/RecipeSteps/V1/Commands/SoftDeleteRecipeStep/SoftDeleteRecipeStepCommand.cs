namespace Food.V2.Application.RecipeSteps.V1.Commands.SoftDeleteRecipeStep;

public record SoftDeleteRecipeStepCommand(string Id,string FoodId):IRequest;