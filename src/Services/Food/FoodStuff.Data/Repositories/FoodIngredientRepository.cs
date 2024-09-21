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
    public class FoodIngredientRepository : Repository<FoodIngredient>, IFoodIngredientRepository, IScopedDependency
    {
        public FoodIngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<IngredientModel>> GetIngsByFoodIdAsync(int foodId)
        {
            var foodIngs = Table
                 .Include(i => i.Ingredient)
                 .ThenInclude(m => m.IngredientMeasureUnits)
                 .ThenInclude(m => m.MeasureUnit)
                .Where(f => f.FoodId == foodId);
            ;
            var ingList = new List<IngredientModel>();
            foreach (var item in foodIngs)
            {
                var ing = new IngredientModel()
                {
                    Id = item.IngredientId,
                    NamId = item.Ingredient.NameId,
                    MeasureUnitId = item.MeasureUnitId,
                    Value = item.IngredientValue,
                    MeasureUnitNameId = item.MeasureUnit.NameId
                };

                var ingMeasures = new List<MeasureValuBase<double>>();
                foreach (var ingMeas in item.Ingredient.IngredientMeasureUnits)
                {
                    var measureUnit = new MeasureValuBase<double>()
                    {
                        MeasureUnitId = ingMeas.MeasureUnit.Id,
                        MeasureUnitNameId = ingMeas.MeasureUnit.NameId
                    };
                    ingMeasures.Add(measureUnit);
                }
                ing.MeasureUnitList = ingMeasures;
                ingList.Add(ing);
            }

            return ingList;
        }

        public async Task<IEnumerable<FoodIngredient>> GetIngsByFoodIdAsync(int foodId, CancellationToken cancellationToken)
        {
            var foodIng = TableNoTracking.Where(f => f.FoodId == foodId);
            return foodIng;
        }
    }
}
