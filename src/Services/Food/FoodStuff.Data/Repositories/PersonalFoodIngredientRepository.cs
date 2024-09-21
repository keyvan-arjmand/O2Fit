using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public class PersonalFoodIngredientRepository : Repository<PersonalFoodIngredient>, IPersonalFoodIngredientRepository, IScopedDependency
    {
        public PersonalFoodIngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<PersonalFoodIngredient>> GetIngsByFoodIdAsync(int foodId,CancellationToken cancellationToken)
        {
            var foodIngs =await Table
                .Include(i => i.Ingredient).ThenInclude(t => t.Translation)
                .Include(i => i.Ingredient).ThenInclude(i => i.IngredientMeasureUnits).ThenInclude(m => m.MeasureUnit)
                .Where(f => f.PersonalFoodId == foodId).ToListAsync(cancellationToken);
            return foodIngs;
        }

    }
}
