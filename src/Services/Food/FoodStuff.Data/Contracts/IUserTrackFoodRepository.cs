using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IUserTrackFoodRepository : IRepository<UserTrackFood>
    {
        Task<int> AddAsync(UserTrackFood userTrackFood, CancellationToken cancellationToken, bool saveNow = true);
        Task<UserTrackFood> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task<List<UserTrackFood>> GetByMealIdAsync(int userId,int foodMealId,DateTime dateTime, CancellationToken cancellationToken, bool saveNow = true);
        Task<List<UserTrackFood>> GetUserMealsByDateAsync(int userId, DateTime dateTime, CancellationToken cancellationToken, bool saveNow = true);
        Task<UserTrackFood> GetBy_IdAsync(CancellationToken cancellationToken, string _id);
        Task<UserTrackFood> GetByIdAsNoTrackingAsync(CancellationToken cancellationToken, int id);

    }
}
