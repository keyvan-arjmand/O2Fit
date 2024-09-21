using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
   public interface IUserFoodFavoriteRepository:IRepository<UserFoodFavorite>
    {
        IQueryable<UserFoodFavorite> GetlistUserFoodFavorite(int UserId, CancellationToken cancellationToken);
        Task<UserFoodFavorite> AddAsync(UserFoodFavorite entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<UserFoodFavorite> GetByFoodAndUserId(int foodId, int userId, CancellationToken cancellationToken);
    }
}
