using Common.Utilities;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
  public interface IPersonalFoodRepository:IRepository<PersonalFood>
    {
        Task<int> AddAsync(PersonalFood personalFood, CancellationToken cancellationToken, bool saveNow = true);
        Task<List<PersonalFood>> GetByUserIdAsync(int userId,CancellationToken cancellationToken, bool saveNow = true);
        Task<PersonalFood> GetByIdAsync(int Id, CancellationToken cancellationToken);
    }
}
