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
    public class UserTrackWorkOutRepository : Repository<UserTrackWorkOut>, IUserTrackWorkOutRepository, IScopedDependency
    {
        public UserTrackWorkOutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<UserTrackWorkOut>> GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var userTrackWorkOut = await (from userWorkouts in Table
                                    .Include(a => a.PersonalWorkOut)
                                    .Include(a => a.WorkOut)
                                          where (userWorkouts.InsertDate == dateTime.Date) && (userWorkouts.UserId == userId)
                                          select userWorkouts).ToListAsync(cancellationToken);
            return userTrackWorkOut;
        }
        public async Task<List<UserTrackWorkOut>> GetByWorkoutId(int WorkoutId, CancellationToken cancellationToken)
        {
            var userTrackWorkOut = TableNoTracking
            .Include(a => a.PersonalWorkOut).
            Include(a => a.WorkOut).Where(n => n.WorkOutId == WorkoutId);
            List<UserTrackWorkOut> result = (userTrackWorkOut != null) ? await userTrackWorkOut.ToListAsync(cancellationToken) : null; ;
            return result;
        }

        public async Task<List<UserTrackWorkOut>> GetUserHistory(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            //var userTrackWorkOut = await TableNoTracking.Where(n => n.InsertDate >= dateTime.Date && n.UserId == userId)
            //.Include(a => a.PersonalWorkOut).
            //Include(a => a.WorkOut)
            //.ToListAsync(cancellationToken);
            var userTrackWorkOut = await (from userWorkouts in Table
                                    .Include(a => a.PersonalWorkOut)
                                    .Include(a => a.WorkOut)
                                          where (userWorkouts.InsertDate >= dateTime.Date) && (userWorkouts.UserId == userId)
                                          select userWorkouts).ToListAsync(cancellationToken);
            return userTrackWorkOut;
        }
    }
}
