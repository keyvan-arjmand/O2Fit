namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientById;

public record GetIngredientByIdQuery(string Id): IRequest<GetIngredientByIdDto>;