using Common;
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
using Dapper;

namespace FoodStuff.Data.Repositories
{
    public class UserTrackNutrientRepository : Repository<UserTrackNutrient>, IUserTrackNutrientRepository, IScopedDependency
    {

        public UserTrackNutrientRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
        }

        public async Task<UserTrackNutrient> GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            //using var conn = await _connectionFactory.CreateConnectionAsync();
            //var userTrackNutrient= TableNoTracking.Where(n=>n.InsertDate.Date==dateTime.Date && n.UserId== userId).FirstOrDefaultAsync(cancellationToken);

            //string query = $"SELECT * from \"UserTrackNutrients\" WHERE \"UserId\" ={userId} AND \"InsertDate\"='{dateTime.Date}'";

           // var userTrackNutrient1 = await conn.QuerySingleOrDefaultAsync<UserTrackNutrient>(query);
           var userTrackNutrient = await 
               TableNoTracking.FirstOrDefaultAsync(x => x.InsertDate.Date == dateTime.Date && x.UserId == userId,
                   cancellationToken);
            return userTrackNutrient;
        }
        public Task<List<UserTrackNutrient>> GetByDateReport(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var userTrackNutrient = TableNoTracking.Where(n => n.InsertDate.Date >= dateTime.Date && n.UserId == userId).ToListAsync(cancellationToken);
            return userTrackNutrient;
        }
    }
}
