using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Entities.Translation;

namespace Data.Contracts
{
    public interface ITranslationRepository : IRepository<Translation>
    {
        Task<Translation> AddAsync(Translation translation, CancellationToken cancellationToken);
    }
}
