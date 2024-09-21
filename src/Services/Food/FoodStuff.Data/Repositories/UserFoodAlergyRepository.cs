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
    class UserFoodAlergyRepository:Repository<UserFoodAlergy>,IUserFoodAlergyRepository,IScopedDependency
    {
        public UserFoodAlergyRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
        public virtual async Task<UserFoodAlergy> AddAsync(UserFoodAlergy entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public  IQueryable<UserFoodAlergy> GetlistUserAlergy(int UserId,CancellationToken cancellationToken)
        {
           var userFoodAlergies = Table.Where(a => a.UserId == UserId);
            return userFoodAlergies;
        }

    }
}
