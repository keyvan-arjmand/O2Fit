namespace Food.V2.Application.Ingredients.V1.Commands.DeleteIngredientById;

public record DeleteIngredientByIdCommand(string Id): IRequest;