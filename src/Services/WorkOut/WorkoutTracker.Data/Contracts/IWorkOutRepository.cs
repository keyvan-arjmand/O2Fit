using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Data.Contracts
{
    public interface IWorkOutRepository:IRepository<WorkOut>
    {
        Task<int> AddAsync(WorkOut workOut, CancellationToken cancellationToken, bool saveNow = true);
        Task<List<WorkOut>> GetByClassificationIdAsync(int classificationId, CancellationToken cancellationToken, bool saveNow = true);

    }
}
