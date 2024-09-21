using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Contracts
{
   public interface IUserFavoriteWorkOutRepository:IRepository<UserFavoriteWorkOut>
    {
        IQueryable<UserFavoriteWorkOut> GetUserFavorites(int userId, CancellationToken cancellationToken);
    }
}
