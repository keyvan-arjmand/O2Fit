namespace Identity.V2.Application.Countries.V1.Queries.GetCountryIdByOldSystemCountryId;

public record GetCountryIdByOldSystemCountryIdQuery(int OldSystemCountryId) : IRequest<string>;