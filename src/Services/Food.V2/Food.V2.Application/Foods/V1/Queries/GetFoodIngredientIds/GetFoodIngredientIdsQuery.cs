namespace Food.V2.Application.Foods.V1.Queries.GetFoodIngredientIds;

public record GetFoodIngredientIdsQuery(string FoodId): IRequest<List<string>>;