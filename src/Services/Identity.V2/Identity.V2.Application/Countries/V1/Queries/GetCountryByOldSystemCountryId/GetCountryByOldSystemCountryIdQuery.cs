namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByOldSystemCountryId;

public record GetCountryByOldSystemCountryIdQuery(int CountryId) : IRequest<CountryDto>;