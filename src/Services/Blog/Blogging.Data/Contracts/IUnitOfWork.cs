using Blogging.Domain.Common;

namespace Data.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
    }
}