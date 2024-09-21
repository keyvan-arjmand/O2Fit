using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Repositories
{
   public class PersonalWorkOutRepository:Repository<PersonalWorkOut>,IPersonalWorkOutRepository,IScopedDependency
    {
        public PersonalWorkOutRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public IQueryable<PersonalWorkOut> GetAsync(int userId,CancellationToken cancellationToken)
        {
            var _list =  Table.Where(a => a.UserId == userId);
            return _list;
        }

       public async Task<int> AddAsync(PersonalWorkOut entity, CancellationToken cancellationToken, bool saveNow)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity.Id;
        }

    }
}
