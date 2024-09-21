using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
   public interface IIngeredientRepository:IRepository<Ingredient>
    {
        Task<int> AddAsync(Ingredient ingredient, CancellationToken cancellationToken);
        Task DeleteAsync(Ingredient Ing, CancellationToken cancellationToken);
    }
}
