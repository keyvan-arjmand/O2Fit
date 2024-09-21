using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodStuff.Domain.Entities.ViewModels;

namespace FoodStuff.Data.Repositories
{
    public class UserTrackFoodRepository : Repository<UserTrackFood>, IUserTrackFoodRepository, IScopedDependency
    {
        public UserTrackFoodRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public virtual async Task<int> AddAsync(UserTrackFood userTrackFood, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(userTrackFood, nameof(userTrackFood));
            await Entities.AddAsync(userTrackFood, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return userTrackFood.Id;
        }

        public async Task<UserTrackFood> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            var userTrackFood = await Table
                .Include(f => f.Food)
                .Include(pf => pf.PersonalFood)
                .Include(m => m.MeasureUnit).FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
                //.Where(f=>f.Id==id).FirstAsync(cancellationToken);
            return userTrackFood;
        }

        public async Task<List<UserTrackFood>> GetByMealIdAsync(int userId, int foodMealId, DateTime dateTime, CancellationToken cancellationToken, bool saveNow = true)
        {
            var result = new List<UserTrackFood>();
            var userTrackFoodList = await Table
                .Include(f => f.Food)
                .Include(pf => pf.PersonalFood)
                .Include(m => m.MeasureUnit)
                .Where(f => f.UserId == userId && f.InsertDate.Date == dateTime.Date && f.FoodMeal == foodMealId).ToListAsync(cancellationToken);
            return userTrackFoodList;
        }
        public async Task<List<UserTrackFood>> GetUserMealsByDateAsync(int userId, DateTime dateTime, CancellationToken cancellationToken, bool saveNow = true)
        {
            var result = new List<UserTrackFood>();
            //var userTrackFoodList = await Table
            //    .Include(f => f.Food)
            //    .Include(pf => pf.PersonalFood)
            //    .Include(m => m.MeasureUnit)
            //    .Where(f => f.UserId == userId && f.InsertDate.Date == dateTime.Date).ToListAsync(cancellationToken);
            var userTrackFoodList = await (from userFoods in Table
                                           .Include(f => f.Food)
                                           .Include(pf => pf.PersonalFood)
                                           .Include(m => m.MeasureUnit)
                                           where (userFoods.InsertDate.Date == dateTime.Date) && (userFoods.UserId == userId)
                                           select userFoods).ToListAsync(cancellationToken);
            return userTrackFoodList;
        }

        public async Task<UserTrackFood> GetBy_IdAsync(CancellationToken cancellationToken, string _id)
        {
            var userTrackFood = await Table
                .Include(f => f.Food)
                .Include(pf => pf.PersonalFood)
                .Include(m => m.MeasureUnit)
                .Where(f => f._id == _id).FirstOrDefaultAsync(cancellationToken);
            return userTrackFood;
        }

        public async Task<UserTrackFood> GetByIdAsNoTrackingAsync(CancellationToken cancellationToken, int id)
        {
            var userTrackFood = await TableNoTracking
                .Include(f => f.Food)
                .Include(pf => pf.PersonalFood)
                .Include(m => m.MeasureUnit)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
            //.Where(f=>f.Id==id).FirstAsync(cancellationToken);
            return userTrackFood;
        }
    }
}
