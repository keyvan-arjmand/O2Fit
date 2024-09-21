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
    public interface IUserTrackSleepRepository:IRepository<UserTrackSleep>
    {
        IQueryable<UserTrackSleep> GetByDays(int userId,int days);
        IQueryable<UserTrackSleep> GetByDate(int userId,DateTime dateTime);
        Task<int> AddAsync(UserTrackSleep entity, CancellationToken cancellationToken, bool saveNow = true);
    }
}
