using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
   public interface IUserTrackNutrientRepository:IRepository<UserTrackNutrient>
    {
        Task<UserTrackNutrient> GetByDate(int userId,DateTime dateTime,CancellationToken cancellationToken);
        Task<List<UserTrackNutrient>> GetByDateReport(int userId,DateTime dateTime,CancellationToken cancellationToken);
    }
}
