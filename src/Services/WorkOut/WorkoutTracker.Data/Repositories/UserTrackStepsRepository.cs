using Common;
using Common.Utilities;
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
   public class UserTrackStepsRepository: Repository<UserTrackSteps>, IUserTrackStepsRepository, IScopedDependency
    {
        public UserTrackStepsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public virtual async Task<List<UserTrackSteps>> GetAsyncByDate(int userId,int daysCount, CancellationToken cancellationToken)
        {
            //farhoudi iquerable
            var dates = DateTime.Now.AddDays(-daysCount).Date;
            var userTrackSteps = await TableNoTracking.Where(s=>s.UserId == userId &&  s.InsertDate.Date >= dates )
                .ToListAsync(cancellationToken);

            List<UserTrackSteps> _list = new List<UserTrackSteps>();


            for (int i=1;i<=daysCount;i++)
            {
                var listStep = userTrackSteps.Where(s => s.InsertDate.Date == dates);
                UserTrackSteps step = new UserTrackSteps()
                {
                     InsertDate = dates,
                     BurnedCalories= (listStep != null) ? listStep.Sum(s => s.BurnedCalories) : 0,
                    StepsCount = (listStep != null) ? listStep.Sum(s=>s.StepsCount):0,
                      
                };
                dates = dates.AddDays(1);

                _list.Add(step);
            }

            return _list;
        }
        public virtual async Task<List<UserTrackSteps>> GetList(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var userTrackSteps = await TableNoTracking.Where(n => n.InsertDate.Date == dateTime.Date && n.UserId == userId).ToListAsync(cancellationToken);
            return userTrackSteps;
        }
        public virtual async Task<int> AddAsync(UserTrackSteps entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity.Id;
        }
   

    }
}
