using Common;
using Data.Contracts;
using Data.Database;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities.Translation;

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
