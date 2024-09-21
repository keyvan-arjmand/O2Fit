using System.Collections;
using Identity.Application.Common.Interface;
using Identity.Domain.Common;
using Identity.Domain.DataBase;

namespace Identity.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    private Hashtable? _repositories;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }

    public IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }
}