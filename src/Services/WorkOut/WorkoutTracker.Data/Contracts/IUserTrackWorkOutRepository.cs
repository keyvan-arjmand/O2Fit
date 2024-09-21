using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Contracts
{
   public interface IUserTrackWorkOutRepository:IRepository<UserTrackWorkOut>
    {
        Task<List<UserTrackWorkOut>> GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken);
        Task<List<UserTrackWorkOut>> GetByWorkoutId(int WorkoutId, CancellationToken cancellationToken);
        Task<List<UserTrackWorkOut>> GetUserHistory(int userId, DateTime dateTime, CancellationToken cancellationToken);
    }
}
