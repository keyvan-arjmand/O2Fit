using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
   public class PersonalFoodRepository:Repository<PersonalFood>,IPersonalFoodRepository, IScopedDependency
    {
        public PersonalFoodRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
        public virtual async Task<int> AddAsync(PersonalFood personalFood, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(personalFood, nameof(personalFood));
          await Entities.AddAsync(personalFood, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return personalFood.Id;
        }

        public Task<List<PersonalFood>> GetByUserIdAsync(int userId, CancellationToken cancellationToken, bool saveNow = true)
        {
            var personalFoodList = Table
                .Include(pi => pi.PersonalFoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
                .Include(pi => pi.PersonalFoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(im => im.IngredientMeasureUnits).ThenInclude(m => m.MeasureUnit)
                .Where(p => p.UserId == userId && p.Isdelete==false).ToListAsync(cancellationToken);
            return personalFoodList;
        }
        public async Task<PersonalFood> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
                var personalFood = await Table
                    .Include(pi => pi.PersonalFoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
                    .Include(pi => pi.PersonalFoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(im => im.IngredientMeasureUnits).ThenInclude(m => m.MeasureUnit)
                    .Where(f => f.Id == Id && f.Isdelete==false).FirstOrDefaultAsync(cancellationToken);
                return personalFood;
        }
    }
}
