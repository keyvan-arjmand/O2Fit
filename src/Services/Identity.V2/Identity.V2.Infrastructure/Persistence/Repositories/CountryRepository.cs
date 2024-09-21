namespace Identity.V2.Infrastructure.Persistence.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(IMediator mediator, IConfiguration configuration) : base(mediator, configuration)
    {
    }

    public async Task<string?> GetCountryIdByOldSystemCountryIdAsync(int oldSystemCountryId,
        CancellationToken cancellationToken = default)
    {
        var id = await Collection.AsQueryable().Where(x => x.CountryId == oldSystemCountryId && x.IsDelete == false)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return id;
    }

    public async Task<int?> GetOldSystemCountryIdByCountryId(string countryId, CancellationToken cancellationToken = default)
    {
        var id = await Collection.AsQueryable().Where(x => x.Id == countryId && x.IsDelete == false)
            .Select(s => s.CountryId).FirstOrDefaultAsync(cancellationToken);
        return id;
    }
}