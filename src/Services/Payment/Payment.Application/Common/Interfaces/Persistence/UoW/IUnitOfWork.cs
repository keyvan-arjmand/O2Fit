using Payment.Application.Common.Interfaces.Persistence.Repositories;
using Payment.Domain.Aggregates.SequenceAggregate;

namespace Payment.Application.Common.Interfaces.Persistence.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
    ISequenceRepository SequenceRepository(); 
    void AddCommand(Func<Task> func);
    Task TransactionAsync();
}