using Common;
using Data.Contracts;
using Identity.Domain.Entities.Translation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Command
{
    public class UpdateTranslationCommandHandler : IRequestHandler<UpdateTranslationCommand, Translation>, ITransientDependency
    {
        private readonly ITranslationRepository _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;

        public UpdateTranslationCommandHandler(ITranslationRepository repository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Translation> Handle(UpdateTranslationCommand request, CancellationToken cancellationToken)
        {
            await _repositoryRedis.UpdateAsync($"Translation_Identity_{request.Translation.Id}", request.Translation);
            await _repository.UpdateAsync(request.Translation, cancellationToken);
            return request.Translation;
        }

    }
}
