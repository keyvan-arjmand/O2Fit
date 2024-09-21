namespace Advertise.Application.Common.Interfaces.Persistence.Repositories;

public interface IGenericRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PaginationResult<T>> GetAllPaginationAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);
  
    Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task<T> GetSingleDocumentByFilterAsync(FilterDefinition<T> filter,
        CancellationToken cancellationToken = default);

    Task<List<T>> GetListOfDocumentsByFilterAsync(FilterDefinition<T> filter,
        CancellationToken cancellationToken = default);
    Task<PaginationResult<T>> GetListPaginationOfDocumentsByFilterAsync(int pageIndex, int pageSize,FilterDefinition<T> filter,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task<long> LongCountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<TResult> MaxAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<TResult> MinAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<decimal> AverageAsync(
        Expression<Func<T, decimal>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<decimal> SumAsync(
        Expression<Func<T, decimal>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task InsertOneAsync(
        T? aggregateRoot,
        InsertOneOptions? options = null,
        CancellationToken cancellationToken = default);
    Task InsertOneManualAuditLogAsync(
        T? aggregateRoot,
        InsertOneOptions? options = null,
        CancellationToken cancellationToken = default);

    Task InsertManyAsync(
        IEnumerable<T> aggregateRoots,
        InsertManyOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<UpdateResult> UpdateOneAsync(
        Expression<Func<T, bool>> predicate,
        T aggregateRoot,
        Expression<Func<T, object>>[] properties,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<UpdateResult> UpdateOneAsync(
        Expression<Func<T, bool>> predicate,
        IDictionary<Expression<Func<T, object>>, object> properties,
        T aggregateRoot,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<UpdateResult> UpdateOneAsync(
        FilterDefinition<T> filter,
        T aggregateRoot,
        UpdateDefinition<T> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<UpdateResult> UpdateManyAsync(
        Expression<Func<T, bool>> predicate,
        IDictionary<Expression<Func<T, object>>, object> properties,
        T aggregateRoot,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default);


    Task<BulkWriteResult<T>> BulkWriteAsync(
        IEnumerable<WriteModel<T>> requests,
        IEnumerable<T> aggregateRoots,
        BulkWriteOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<ReplaceOneResult> ReplaceOneAsync(
        Expression<Func<T, bool>> predicate,
        T aggregateRoot,
        ReplaceOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<DeleteResult> DeleteOneAsync(
        Expression<Func<T, bool>> predicate,
        T aggregateRoot,
        DeleteOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<DeleteResult> DeleteManyAsync(
        Expression<Func<T, bool>> predicate,
        IEnumerable<T> aggregateRoots,
        DeleteOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<UpdateResult> SoftDeleteByIdAsync(string id, T aggregateRoot, UpdateOptions? options = null,
        CancellationToken cancellationToken = default);

}