namespace Food.V2.Application.IngredientAllergyCategories.V1.Queries.GetIngredientAllergyIds;

public record GetIngredientAllergyIdsQuery(string IngredientId) : IRequest<string>;