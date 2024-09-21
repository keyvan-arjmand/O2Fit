namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddIngredientAllergyToRoot;

public record AddIngredientAllergyToRootCommand(string RootAllergyId, string IngredientAllergyId) : IRequest;