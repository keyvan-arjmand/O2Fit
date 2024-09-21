using Common;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Repositories
{
    public class UserFavoriteWorkOutRepository : Repository<UserFavoriteWorkOut>, IUserFavoriteWorkOutRepository, IScopedDependency
    {
        public UserFavoriteWorkOutRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
        public IQueryable<UserFavoriteWorkOut> GetUserFavorites(int userId, CancellationToken cancellationToken)
        {
            var _list = Table.Where(a => a.UserId == userId).Include(a=>a.WorkOut);
            return _list;
        }
    }
}
