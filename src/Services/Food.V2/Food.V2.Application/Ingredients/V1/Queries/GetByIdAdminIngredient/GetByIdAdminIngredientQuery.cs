namespace Food.V2.Application.Ingredients.V1.Queries.GetByIdAdminIngredient;

public record GetByIdAdminIngredientQuery(string Id) : IRequest<GetByIdAdminIngredientDto>;