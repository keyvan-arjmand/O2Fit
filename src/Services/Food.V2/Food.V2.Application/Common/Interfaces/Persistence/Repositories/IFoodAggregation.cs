namespace Food.V2.Application.Common.Interfaces.Persistence.Repositories;

public interface IFoodAggregation : IGenericRepository<Domain.Aggregates.FoodAggregate.Food>
{
    Task<FoodWithDetailDto> GetFoodWithDetailById(string id, CancellationToken cancellationToken = default);

    Task<FoodWithDetailDto> GetSingleDocumentByFilterAsync(
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter,
        CancellationToken cancellationToken = default);

    Task<List<FoodWithDetailDto>> GetListOfDocumentsByFilterAsync(
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter,
        CancellationToken cancellationToken = default);

    Task<List<FoodWithDetailDto>> GetListPaginationOfDocumentsByFilterAsync(int pageIndex, int pageSize,
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter,
        CancellationToken cancellationToken = default);

    Task<List<FoodWithDetailDto>> GetListPaginationOfDocumentsAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);
}