using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Repositories
{
   public class WorkoutAttributeRepository:Repository<WorkOutAttribute>,IWorkoutAttributeRepository,IScopedDependency
    {
        public WorkoutAttributeRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
      async Task<int> IWorkoutAttributeRepository.AddAsync(WorkOutAttribute workOutAttribute, CancellationToken cancellationToken, bool saveNow)
        {
            Assert.NotNull(workOutAttribute, nameof(workOutAttribute));
            await Entities.AddAsync(workOutAttribute, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return workOutAttribute.Id;
        }

    }
}
