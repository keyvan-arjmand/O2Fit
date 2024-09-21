namespace Food.V2.Application.Foods.V1.Queries.GetFoodNameById;

public record GetFoodNameByIdQuery(string Id) : IRequest<FoodTranslationDto>;