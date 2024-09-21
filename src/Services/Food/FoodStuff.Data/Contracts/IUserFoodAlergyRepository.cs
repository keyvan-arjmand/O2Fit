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
   public interface IUserFoodAlergyRepository:IRepository<UserFoodAlergy>
    {
        Task<UserFoodAlergy> AddAsync(UserFoodAlergy entity, CancellationToken cancellationToken, bool saveNow = true);
        IQueryable<UserFoodAlergy> GetlistUserAlergy(int UserId, CancellationToken cancellationToken);
    }
}
