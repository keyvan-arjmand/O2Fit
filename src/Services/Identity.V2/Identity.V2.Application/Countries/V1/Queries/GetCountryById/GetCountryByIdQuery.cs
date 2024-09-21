namespace Identity.V2.Application.Countries.V1.Queries.GetCountryById;

public record GetCountryByIdQuery(string Id) : IRequest<CountryDto>;
