namespace Food.V2.Application.DietPacks.V1.Queries.GetDietPackById;

public record GetDietPackByIdQuery(string Id) : IRequest<DietPackDto>;