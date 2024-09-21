using Data.Contracts;
using SocialMessaging.Domain.Entities.Translation;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Data.Contracts
{
    public interface ITranslationRepository : IRepository<TranslationDto>
    {
        Task<TranslationDto> AddAsync(TranslationDto translation, CancellationToken cancellationToken);

    }
}
