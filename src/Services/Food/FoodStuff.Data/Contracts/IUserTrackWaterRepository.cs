using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
   public interface IUserTrackWaterRepository:IRepository<UserTrackWater>
    {
        Task<List<UserTrackWater>> GetTrackWaterAsync(DateTime? StartDate, DateTime? EndDate, int userId, CancellationToken cancellationToken);
        Task<UserTrackWater> SetTrackWaterAsync(DateTime createdate, int userId, double value,string _Id, CancellationToken cancellationToken);
        Task<int> AddAsync(UserTrackWater entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<List<UserTrackWater>> GetUserTrackWaterHistory(int userId, DateTime dateTime, CancellationToken cancellationToken);
    }
}
