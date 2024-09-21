using Common;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Repositories
{
    public class BurnedWorkOutCaloriesRepository : Repository<BurnedWorkOutCalories>, IBurnedWorkOutCaloriesRepository, IScopedDependency
    {
        public BurnedWorkOutCaloriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        async Task<BurnedWorkOutCalories> IBurnedWorkOutCaloriesRepository.GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            return await TableNoTracking.FirstOrDefaultAsync(n => n.InsertDate.Date == dateTime.Date && n.UserId == userId, cancellationToken).ConfigureAwait(false);

        }






    }
}
