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
    public class FoodMeasureUnitRepository : Repository<FoodMeasureUnit>, IFoodMeasureUnitRepository, IScopedDependency
    {
        public FoodMeasureUnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<FoodMeasureUnit>> GetFoodMeasureUnits(int foodId, CancellationToken cancellationToken)
        {
            var foodMeasureunits = TableNoTracking.Where(fm => fm.FoodId == foodId).AsEnumerable();
            return foodMeasureunits;
        }
    }
}
