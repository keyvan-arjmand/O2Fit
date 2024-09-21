using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace Food.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : RepositoryBase<T>, IGenericRepository<T> where T : class
{
    public GenericRepository(FoodContext dbContext) : base(dbContext)
    {
    }

  
}