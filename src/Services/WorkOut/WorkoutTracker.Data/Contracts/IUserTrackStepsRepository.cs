using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Contracts
{
   public interface IUserTrackStepsRepository:IRepository<UserTrackSteps>
    {
        Task<List<UserTrackSteps>> GetList(int userId, DateTime dateTime, CancellationToken cancellationToken);
        Task<List<UserTrackSteps>> GetAsyncByDate(int userId, int daysCount, CancellationToken cancellationToken);
        Task<int> AddAsync(UserTrackSteps entity, CancellationToken cancellationToken, bool saveNow = true);
    }
}
