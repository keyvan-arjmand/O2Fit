using Identity.Application.Common.Interface;
using Identity.Domain.Common;
using Identity.Domain.DataBase;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Identity.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : AggregateRoot
{
    private readonly ApplicationDbContext _dbContext;
    public DbSet<T> Entities { get; }
    public IQueryable<T> Table => Entities;
    public IQueryable<T> TableNoTracking => Entities.AsNoTracking();
    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Entities = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        Assert.NotNull(entities);
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        Entities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity);
        Entities.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}