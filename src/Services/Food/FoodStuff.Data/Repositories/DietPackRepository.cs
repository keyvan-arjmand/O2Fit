using Common;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
   public class DietPackRepository: Repository<DietPack>, IDietPackRepository, IScopedDependency
    {
        public DietPackRepository(ApplicationDbContext dbContext)
         : base(dbContext)
        {
        }

        public async Task<int> AddAsync(DietPack dietPack, CancellationToken cancellationToken)
        {
            await base.AddAsync(dietPack, cancellationToken).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return dietPack.Id;
        }
    }
}
