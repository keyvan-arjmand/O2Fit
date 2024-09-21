using Common;
using Data.Contracts;
using MediatR;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.Translation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Command
{
    public class UpdateTranslationCommandHandler : IRequestHandler<UpdateTranslationCommand, TranslationDto>, ITransientDependency
    {
        private readonly ITranslationRepository _repository;
        private readonly IRepositoryRedis<TranslationDto> _repositoryRedis;

        public UpdateTranslationCommandHandler(ITranslationRepository repository, IRepositoryRedis<TranslationDto> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<TranslationDto> Handle(UpdateTranslationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repositoryRedis.UpdateAsync($"Translation_Social_{request.Translation.Id}", request.Translation);
                _repository.Detach(request.Translation);
                await _repository.UpdateAsync(request.Translation, cancellationToken);
                return request.Translation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
