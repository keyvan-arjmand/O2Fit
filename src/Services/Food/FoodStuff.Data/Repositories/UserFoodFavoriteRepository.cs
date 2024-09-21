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
    public class UserFoodFavoriteRepository : Repository<UserFoodFavorite>, IUserFoodFavoriteRepository, IScopedDependency
    {
        public UserFoodFavoriteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<UserFoodFavorite> GetlistUserFoodFavorite(int UserId, CancellationToken cancellationToken)
        {
            var userFoodAlergies = Table.Where(a => a.UserId == UserId);
            return userFoodAlergies;
        }

      public async Task<UserFoodFavorite> AddAsync(UserFoodFavorite entity, CancellationToken cancellationToken, bool saveNow)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;

        }

        public async Task<UserFoodFavorite> GetByFoodAndUserId(int foodId, int userId, CancellationToken cancellationToken)
        {
            var userFoodFavorite = await TableNoTracking.Where(f => f.UserId == userId && f.FoodId == foodId).FirstAsync(cancellationToken);
            return userFoodFavorite;
        }
    }
}
