namespace Notification.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : AggregateRoot
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    public IMongoCollection<T> Collection { get; }
    private readonly IMongoClient _mongoClient;
    private readonly ICurrentUserService _currentUserService;

    public GenericRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _configuration = configuration;
        _currentUserService = currentUserService;

        var settings = MongoClientSettings.FromConnectionString(_configuration["MongoSettings:ConnectionString"]);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _mongoClient = new MongoClient(settings);
        Collection = _mongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"])
            .GetCollection<T>(ToLowerFirsWord(typeof(T).Name) + "s");
    }

    public Task<T> GetSingleDocumentByFilterAsync(FilterDefinition<T> filter,
        CancellationToken cancellationToken = default)
    {
        return Collection.Find(filter).Limit(1).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<T>> GetListOfDocumentsByFilterAsync(FilterDefinition<T> filter,
        CancellationToken cancellationToken = default)
    {
        return Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public Task<List<T>> GetListPaginationOfDocumentsByFilterAsync(int pageIndex, int pageSize,
        FilterDefinition<T> filter,
        CancellationToken cancellationToken = default)
    {
        return Collection.Find(filter)
            .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await Collection.FindAsync(x => x.Id == id && !x.IsDelete, null, cancellationToken)
            .ConfigureAwait(false);

        if (result is null)
            return null;

        return result.FirstOrDefault();
    }


    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var documents = await Collection.FindAsync(x => !x.IsDelete, null, cancellationToken);
        return await documents.ToListAsync(cancellationToken);
    }

    public async Task<PaginationResult<T>> GetAllPaginationAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var documents = await Collection.Find(x => !x.IsDelete).SortBy(x => x.Id)
            .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await CountAsync(null, cancellationToken);
        var result = PaginationResult<T>.CreatePaginationResult(pageIndex, pageSize, totalCount, documents);

        return result;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.AnyAsync<T>(predicate, cancellationToken)
            : await source.AnyAsync<T>(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.CountAsync<T>(predicate, cancellationToken)
            : await source.CountAsync<T>(cancellationToken);
    }

    public async Task<long> LongCountAsync(Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.LongCountAsync<T>(predicate, cancellationToken)
            : await source.LongCountAsync<T>(cancellationToken);
    }

    public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector), "selector cannot be null.");
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.Where<T>(predicate).MaxAsync<T, TResult>(selector, cancellationToken)
            : await source.MaxAsync<T, TResult>(selector, cancellationToken);
    }

    public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector), "selector cannot be null.");
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.Where<T>(predicate).MinAsync<T, TResult>(selector, cancellationToken)
            : await source.MinAsync<T, TResult>(selector, cancellationToken);
    }

    public async Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector), "selector cannot be null.");
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.Where<T>(predicate).AverageAsync<T>(selector, cancellationToken)
            : await source.AverageAsync<T>(selector, cancellationToken);
    }

    public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector), "selector cannot be null.");
        IMongoQueryable<T> source = Collection.AsQueryable<T>();
        return predicate != null
            ? await source.Where<T>(predicate).SumAsync<T>(selector, cancellationToken)
            : await source.SumAsync<T>(selector, cancellationToken);
    }

    public async Task InsertOneAsync(T? aggregateRoot, InsertOneOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (aggregateRoot is null)
            throw new ArgumentException("aggregateRoot cannot be null or empty.", nameof(aggregateRoot));

        _currentUserService.InsertAuditLog(aggregateRoot);

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        await Collection.InsertOneAsync(aggregateRoot, options, cancellationToken);
    }

    public async Task InsertManyAsync(IEnumerable<T> aggregateRoots, InsertManyOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        _currentUserService.InsertAuditLog(aggregateRoots);
        await _mediator.DispatchDomainEventsAsync(aggregateRoots);
        await Collection.InsertManyAsync(aggregateRoots, options, cancellationToken);
    }

    public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> predicate, T aggregateRoot,
        Expression<Func<T, object>>[] properties, UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");
        if ((object)aggregateRoot is null)
            throw new ArgumentNullException(nameof(aggregateRoot), "entity cannot be null.");
        if (properties is null || !properties.Any())
            throw new ArgumentException("properties cannot be null or empty.", nameof(properties));
        UpdateDefinition<T>? definition = (UpdateDefinition<T>)null;
        foreach (Expression<Func<T, object>> property in properties)
        {
            object obj = property.Compile()(aggregateRoot);
            definition = definition != null
                ? definition.Set<T, object>(property, obj)
                : Builders<T>.Update.Set<object>(property, obj);
        }

        #region AuditLog

        definition = definition.Set(x => x.LastModified, DateTime.UtcNow);
        definition = definition.Set(x => x.LastModifiedBy, _currentUserService.Username);
        definition = definition.Set(x => x.LastModifiedById, ObjectId.Parse(_currentUserService.UserId));

        #endregion

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await Collection.UpdateOneAsync(predicate, definition, options, cancellationToken);
    }

    public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> predicate,
        IDictionary<Expression<Func<T, object>>, object> properties,
        T aggregateRoot, UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");
        if (properties is null || !properties.Any())
            throw new ArgumentException("properties cannot be null or empty.", nameof(properties));
        UpdateDefinition<T>? definition = (UpdateDefinition<T>)null;
        foreach (KeyValuePair<Expression<Func<T, object>>, object> property in
                 (IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>>)properties)
            definition = definition != null
                ? definition.Set<T, object>(property.Key, property.Value)
                : Builders<T>.Update.Set<object>(property.Key, property.Value);

        #region AuditLog

        definition = definition.Set(x => x.LastModified, DateTime.UtcNow);
        definition = definition.Set(x => x.LastModifiedBy, _currentUserService.Username);
        definition = definition.Set(x => x.LastModifiedById, ObjectId.Parse(_currentUserService.UserId));

        #endregion

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await Collection.UpdateOneAsync(predicate, definition, options, cancellationToken);
    }

    public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, T aggregateRoot,
        UpdateDefinition<T> update, UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        #region AuditLog

        update = update.Set(x => x.LastModified, DateTime.UtcNow);
        update = update.Set(x => x.LastModifiedBy, _currentUserService.Username);
        update = update.Set(x => x.LastModifiedById, ObjectId.Parse(_currentUserService.UserId));

        #endregion

        await _mediator.DispatchDomainEventsAsync(aggregateRoot).ConfigureAwait(false);
        return await Collection.UpdateOneAsync(filter, update, options, cancellationToken).ConfigureAwait(false);
    }

    public async Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> predicate,
        IDictionary<Expression<Func<T, object>>, object> properties, T aggregateRoot, UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");
        if (properties is null || !properties.Any())
            throw new ArgumentException("properties cannot be null or empty.", nameof(properties));
        UpdateDefinition<T>? definition = (UpdateDefinition<T>)null;
        foreach (KeyValuePair<Expression<Func<T, object>>, object> property in
                 (IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>>)properties)
        {
            definition = definition != null
                ? definition.Set<T, object>(property.Key, property.Value)
                : Builders<T>.Update.Set<object>(property.Key, property.Value);

            #region AuditLog

            definition = definition.Set(x => x.LastModified, DateTime.UtcNow);
            definition = definition.Set(x => x.LastModifiedBy, _currentUserService.Username);
            definition = definition.Set(x => x.LastModifiedById, ObjectId.Parse(_currentUserService.UserId));

            #endregion
        }

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await Collection.UpdateManyAsync(predicate, definition, options, cancellationToken);
    }

    public async Task<BulkWriteResult<T>> BulkWriteAsync(IEnumerable<WriteModel<T>> requests,
        IEnumerable<T> aggregateRoots,
        BulkWriteOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (requests is null || !requests.Any())
            throw new ArgumentNullException(nameof(requests), "requests cannot be null or empty.");

        await _mediator.DispatchDomainEventsAsync(aggregateRoots);
        return await Collection.BulkWriteAsync(requests, options, cancellationToken);
    }

    public async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> predicate, T aggregateRoot,
        ReplaceOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");
        if (aggregateRoot is null)
            throw new ArgumentNullException(nameof(aggregateRoot), "aggregateRoot cannot be null.");

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await Collection.ReplaceOneAsync(predicate, aggregateRoot, options, cancellationToken);
    }

    public async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> predicate, T aggregateRoot,
        DeleteOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");

        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await Collection.DeleteOneAsync(predicate, options, cancellationToken);
    }


    public async Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> predicate, IEnumerable<T> aggregateRoots,
        DeleteOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "predicate cannot be null.");
        await _mediator.DispatchDomainEventsAsync(aggregateRoots);
        return await Collection.DeleteManyAsync(predicate, options, cancellationToken);
    }

    public async Task<UpdateResult> SoftDeleteByIdAsync(string id, T aggregateRoot, UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        aggregateRoot.IsDelete = true;
        await _mediator.DispatchDomainEventsAsync(aggregateRoot);
        return await UpdateOneAsync(x => x.Id == id, aggregateRoot,
            new Expression<Func<T, object>>[] { x => x.IsDelete }, null, cancellationToken);
    }

    private string ToLowerFirsWord(string word)
    {
        word = word.Replace(word.Substring(0, 1), word.Substring(0, 1).ToLower());
        return word;
    }
}