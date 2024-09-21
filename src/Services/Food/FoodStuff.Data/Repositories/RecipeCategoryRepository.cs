using Common;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Data.Repositories
{
    public class RecipeCategoryRepository : Repository<RecipeCategore>, IRecipeCategory, IScopedDependency
    {
        public RecipeCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<RecipeCategore> GetByIdAsync(CancellationToken cancellationToken, int ids)
        {
            //var recipe = Table.
            //    .Where(x => x.Id == ids).FirstOrDefault();
            var recipe = await TableNoTracking.FirstOrDefaultAsync(x => x.Id == ids,cancellationToken);
            return recipe;
        }
        public async Task<List<RecipeCategore>> GetAll(CancellationToken cancellationToken)
        {
            //return Table.ToList();
            return await TableNoTracking.ToListAsync(cancellationToken);

        }
    }
}
