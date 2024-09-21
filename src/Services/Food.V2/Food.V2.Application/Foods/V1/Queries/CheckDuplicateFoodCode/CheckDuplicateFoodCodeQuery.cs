namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateFoodCode;

public record CheckDuplicateFoodCodeQuery(string FoodCode) : IRequest<bool>;