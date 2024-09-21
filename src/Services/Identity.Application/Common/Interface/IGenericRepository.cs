
using Identity.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Interface;

public interface IGenericRepository<T> where T : AggregateRoot
{
    DbSet<T> Entities { get; }
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    ValueTask<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
}