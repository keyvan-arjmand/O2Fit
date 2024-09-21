namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExitsById;

public record IsDietPackExitsByIdQuery(string Id): IRequest<bool>;