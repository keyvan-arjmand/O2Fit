using Common;
using Data.Database;
using Data.Repositories;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.Translation;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Data.Repositories
{
    public class TranslationRepository : Repository<TranslationDto>, ITranslationRepository, IScopedDependency
    {
        public TranslationRepository(ApplicationDbContext dbContext)
           : base(dbContext)
        {
        }

        public async Task<TranslationDto> AddAsync(TranslationDto translation, CancellationToken cancellationToken)
        {
            await Entities.AddAsync(translation, cancellationToken).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return translation;
        }


    }
}
