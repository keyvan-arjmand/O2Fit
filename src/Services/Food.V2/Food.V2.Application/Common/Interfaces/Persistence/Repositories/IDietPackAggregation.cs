namespace Food.V2.Application.Common.Interfaces.Persistence.Repositories;

public interface IDietPackAggregation : IGenericRepository<DietPack>
{
    Task<List<GetUserPackageAggregationResultDto>> GetUserPackageAsync(string dietPackCategoryId, int dailyCalorie,
        List<string>? allergyIds, CancellationToken cancellationToken = default(CancellationToken));
}