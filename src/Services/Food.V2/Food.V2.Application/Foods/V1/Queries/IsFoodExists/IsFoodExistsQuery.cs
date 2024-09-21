namespace Food.V2.Application.Foods.V1.Queries.IsFoodExists;

public record IsFoodExistsQuery(string Id): IRequest<bool>;