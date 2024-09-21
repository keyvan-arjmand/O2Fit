using Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
   public interface IDietPackRepository: IRepository<DietPack>
    {
        Task<int> AddAsync(DietPack dietPack, CancellationToken cancellationToken);
       
    }
}
