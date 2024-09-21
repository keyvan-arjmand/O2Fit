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
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Data.Repositories
{
    public class WorkOutRepository:Repository<WorkOut>,IWorkOutRepository,IScopedDependency
    {
        public WorkOutRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public async Task<List<WorkOut>> GetByClassificationIdAsync(int classificationId, CancellationToken cancellationToken, bool saveNow = true)
        {
            var workouts =await Table.Where(w => w.Classification == (Classification)classificationId).ToListAsync(cancellationToken);
            return workouts;
        }

        public async Task<int> AddAsync(WorkOut workOut, CancellationToken cancellationToken, bool saveNow=true)
        {
            Assert.NotNull(workOut, nameof(workOut));
            await Entities.AddAsync(workOut, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return workOut.Id;
        }

       
    }
}
