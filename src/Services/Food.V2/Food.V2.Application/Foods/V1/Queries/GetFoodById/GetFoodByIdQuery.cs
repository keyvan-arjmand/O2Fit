namespace Food.V2.Application.Foods.V1.Queries.GetFoodById;

public record GetFoodByIdQuery(string Id) : IRequest<FoodWithDetailDto>;