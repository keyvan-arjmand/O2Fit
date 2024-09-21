
using Identity.Domain.Common;

namespace Identity.Application.Common.Interface;

public interface IUnitOfWork
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
}