using Common;
using Data.Database;
using Data.Repositories;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
   public class IngeredientRepository: Repository<Ingredient>, IIngeredientRepository, IScopedDependency
    {
        public IngeredientRepository(ApplicationDbContext dbContext)
         : base(dbContext)
        {
        }

        public async Task<int> AddAsync(Ingredient ingredient, CancellationToken cancellationToken)
        {
            await base.AddAsync(ingredient, cancellationToken).ConfigureAwait(false);
              await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return ingredient.Id;
        }

        public async Task DeleteAsync(Ingredient Ing, CancellationToken cancellationToken)
        {
           
            Ing.IngredientMeasureUnits.Clear();
            await base.DeleteAsync(Ing,cancellationToken);
          await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
