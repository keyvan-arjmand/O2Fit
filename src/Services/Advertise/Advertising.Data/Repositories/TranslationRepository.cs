using Advertising.Domain.Entities.Translation;
using Common;
using Data.Contracts;
using Data.Database;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TranslationRepository : Repository<Translation>, ITranslationRepository, IScopedDependency
    {
        public TranslationRepository(ApplicationDbContext dbContext)
           : base(dbContext)
        {
        }

        public async Task<Translation> AddAsync(Translation translation, CancellationToken cancellationToken)
        {
            await Entities.AddAsync(translation, cancellationToken).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return translation;
        }

        
    }
}
