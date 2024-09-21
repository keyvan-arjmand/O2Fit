using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Repositories
{
    public class UserTrackSleepRepository : Repository<UserTrackSleep>, IUserTrackSleepRepository, IScopedDependency
    {
        public UserTrackSleepRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<UserTrackSleep> GetByDays(int userId, int days)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            //// var userTrackSleeps = TableNoTracking.Where(s => s.InsertDate.Date >= dateTime.Date && s.UserId == userId);
            //var userTrackSleeps = from userSleeps in TableNoTracking
            //                      where (userSleeps.InsertDate.Date >= dateTime.Date) && (userSleeps.UserId == userId)
            //                      select userSleeps;
            return TableNoTracking.Where(x => x.InsertDate.Date >= dateTime.Date && x.UserId == userId);

        }
        public IQueryable<UserTrackSleep> GetByDate(int userId, DateTime dateTime)
        {
            var userTrackSleeps = TableNoTracking.Where(s => s.InsertDate.Date == dateTime.Date && s.UserId == userId);
            return userTrackSleeps;
        }

        public virtual async Task<int> AddAsync(UserTrackSleep entity, CancellationToken cancellationToken, bool saveNow)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity.Id;
        }
    }
}
