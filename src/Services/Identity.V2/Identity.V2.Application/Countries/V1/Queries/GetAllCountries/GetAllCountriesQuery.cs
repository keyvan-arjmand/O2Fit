namespace Identity.V2.Application.Countries.V1.Queries.GetAllCountries;

public record GetAllCountriesQuery() : IRequest<List<CountryDto>>;