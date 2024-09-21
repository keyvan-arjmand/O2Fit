namespace Identity.V2.Application.Countries.V1.Queries.GetCountryByCurrencyCode;

public record GetCountryByCurrencyCodeQuery(string CurrencyCode): IRequest<CountryDto>;