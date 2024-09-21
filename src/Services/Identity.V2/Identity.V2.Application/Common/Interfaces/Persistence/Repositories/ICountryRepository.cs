namespace Identity.V2.Application.Common.Interfaces.Persistence.Repositories;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<string?> GetCountryIdByOldSystemCountryIdAsync(int oldSystemCountryId,
        CancellationToken cancellationToken = default);
    
    Task<int?> GetOldSystemCountryIdByCountryId(string countryId,CancellationToken cancellationToken = default);
}