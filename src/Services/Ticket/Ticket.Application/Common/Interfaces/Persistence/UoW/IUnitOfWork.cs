namespace Ticket.Application.Common.Interfaces.Persistence.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
    void AddCommand(Func<Task> func);
    Task TransactionAsync();
}