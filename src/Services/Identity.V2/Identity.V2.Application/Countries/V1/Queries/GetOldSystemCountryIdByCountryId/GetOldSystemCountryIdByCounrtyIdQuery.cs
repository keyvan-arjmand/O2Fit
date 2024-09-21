namespace Identity.V2.Application.Countries.V1.Queries.GetOldSystemCountryIdByCountryId;

public record GetOldSystemCountryIdByCountryIdQuery(string CountryId): IRequest<int>;