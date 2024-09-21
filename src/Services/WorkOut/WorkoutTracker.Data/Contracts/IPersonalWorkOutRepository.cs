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
   public interface IPersonalWorkOutRepository:IRepository<PersonalWorkOut>
    {
        IQueryable<PersonalWorkOut> GetAsync(int userId, CancellationToken cancellationToken);
        Task<int> AddAsync(PersonalWorkOut entity, CancellationToken cancellationToken, bool saveNow = true);
    }
}
