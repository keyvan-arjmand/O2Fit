using Common;
using Data.Contracts;
using MediatR;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.Translation;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Command
{
    public class CreateTranslationCommandHandler : IRequestHandler<CreateTranslationCommand, TranslationDto>, IScopedDependency
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepositoryRedis<TranslationDto> _repositoryRedis;
        public CreateTranslationCommandHandler(
            ITranslationRepository translationRepository, IRepositoryRedis<TranslationDto> repositoryRedis)
        {
            _translationRepository = translationRepository;
            _repositoryRedis = repositoryRedis;
        }


        public async Task<TranslationDto> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            var _translation = await _translationRepository.AddAsync(request.Translation, cancellationToken);
            await _repositoryRedis.UpdateAsync($"Translation_Social_{_translation.Id}", _translation);
            return _translation;
        }
    }
}
