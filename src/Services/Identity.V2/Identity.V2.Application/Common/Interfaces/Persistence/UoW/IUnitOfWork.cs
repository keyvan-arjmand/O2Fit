using Identity.V2.Domain.Common.Role;

namespace Identity.V2.Application.Common.Interfaces.Persistence.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
    IUserGenericRepository<T> UserGenericRepository<T>() where T : UserAggregateRoot;
    IRoleGenericRepository<T> RoleGenericRepository<T>() where T : RoleAggregateRoot;
    ICountryRepository CountryRepository();
    void AddCommand(Func<Task> func);
    Task TransactionAsync();
}