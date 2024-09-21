using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Command
{
    public class CreateTranslationCommandHandler : IRequestHandler<CreateTranslationCommand, Translation>, IScopedDependency
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;
        public CreateTranslationCommandHandler(
            ITranslationRepository translationRepository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _translationRepository = translationRepository;
            _repositoryRedis = repositoryRedis;
        }


        public async Task<Translation> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            var _translation = await _translationRepository.AddAsync(request.Translation, cancellationToken);
            await _repositoryRedis.UpdateAsync($"Translation_Food_{_translation.Id}", _translation);
            return _translation;
        }
    }
}
