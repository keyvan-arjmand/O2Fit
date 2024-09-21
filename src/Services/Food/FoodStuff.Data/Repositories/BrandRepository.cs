using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository, IScopedDependency
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public virtual async Task<Brand> AddAsync(Brand brand, bool saveNow = true)
        {
            try
            {
                Assert.NotNull(brand, nameof(brand));
                await Entities.AddAsync(brand).ConfigureAwait(false);
                if (saveNow)
                    await DbContext.SaveChangesAsync().ConfigureAwait(false);
                return brand;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
