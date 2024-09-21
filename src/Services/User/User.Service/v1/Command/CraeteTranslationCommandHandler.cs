using Data.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Repositories;
using User.Domain.Entities.Translation;

namespace Service.v1.Command
{
    public class CraeteTranslationCommandHandler : IRequestHandler<CraeteTranslationCommand, Translation>, ITransientDependency
    {
        private readonly ITranslationRepository _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;
        public CraeteTranslationCommandHandler(ITranslationRepository repository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Translation> Handle(CraeteTranslationCommand request, CancellationToken cancellationToken)
        {
            var _translation = await _repository.AddAsync(request.Translation, cancellationToken);
            await _repositoryRedis.UpdateAsync($"Translation_User_{_translation.Id}", _translation);
            return _translation;
        }
    }
}
