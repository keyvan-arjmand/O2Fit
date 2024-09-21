using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Contracts
{
   public interface IBurnedWorkOutCaloriesRepository:IRepository<BurnedWorkOutCalories>
    {
     Task<BurnedWorkOutCalories> GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken);
    }
}
