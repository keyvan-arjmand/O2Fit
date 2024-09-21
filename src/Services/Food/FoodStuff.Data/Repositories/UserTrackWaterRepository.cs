using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public class UserTrackWaterRepository : Repository<UserTrackWater>, IUserTrackWaterRepository, IScopedDependency
    {
        public UserTrackWaterRepository(ApplicationDbContext dbContext)
        : base(dbContext)
        {
        }

        public virtual async Task<int> AddAsync(UserTrackWater entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity.Id;
        }

        public async Task<List<UserTrackWater>> GetTrackWaterAsync(DateTime? StartDate, DateTime? EndDate, int userId,
            CancellationToken cancellationToken)
        {

            DateTime _date = DateTime.Now.Date;

            switch (StartDate)
            {
                // اگر دو تا خالی بود فقط روز جاری  رو نشون میده 
                case null when EndDate == null:
                    return await TableNoTracking.Where(u => u.InsertDate.Date == _date && u.UserId == userId)
                         .ToListAsync(cancellationToken);
                    break;

                // اگر استارت خالی بود از تاریخ روز جاری تا انددیت رو نشون میده 
                case null:
                    return await TableNoTracking.Where(u => _date <= u.InsertDate.Date && u.InsertDate.Date <= ((DateTime)EndDate).Date
                          && u.UserId == userId).ToListAsync(cancellationToken);
                    break;

                // اگر انددیت خالی بود از تاریخ استارت تا روز جاری رو نشون میده 
                default:
                    {
                        if (EndDate == null)
                        {
                            return await TableNoTracking.Where(u => ((DateTime)StartDate).Date <= u.InsertDate.Date && u.InsertDate.Date <= _date
                                  && u.UserId == userId).ToListAsync(cancellationToken);
                        }
                        else
                        {
                            return await TableNoTracking.Where(u => ((DateTime)StartDate).Date <= u.InsertDate.Date && u.InsertDate.Date <= ((DateTime)EndDate).Date && u.UserId == userId).ToListAsync(cancellationToken);
                        }
                        break;
                    }
            }
        }

        public async Task<UserTrackWater> SetTrackWaterAsync(DateTime createdate, int userId, double value, string _id, CancellationToken cancellationToken)
        {

            //UserTrackWater _new = new UserTrackWater();
            //_new = await TableNoTracking.Where(u => u.InsertDate.Date == createdate.Date && u.UserId == userId).FirstOrDefaultAsync();
            //if(_new == null)
            //{
            UserTrackWater userTrackWater = new UserTrackWater()
            {
                Value = value,
                UserId = userId,
                InsertDate = createdate,
                _id = _id,
            };
            userTrackWater.Id = await AddAsync(userTrackWater, cancellationToken);
            //_new = userTrackWater;
            //}
            //else
            //{
            //    _new.Value = _new.Value + value;
            //    _new._id = _id;
            //    await base.UpdateAsync(_new, cancellationToken);
            //}
            return userTrackWater;
        }

        public async Task<List<UserTrackWater>> GetUserTrackWaterHistory(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            // var userTrackWaters = await TableNoTracking.Where(w => w.InsertDate >= dateTime && w.UserId == userId).ToListAsync(cancellationToken);
            return await TableNoTracking.Where(x => x.InsertDate >= dateTime && x.UserId == userId).ToListAsync(cancellationToken);

        }
    }
}
