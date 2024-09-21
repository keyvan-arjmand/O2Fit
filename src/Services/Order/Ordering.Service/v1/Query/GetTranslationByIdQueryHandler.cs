using Common;
using Data.Contracts;
using MediatR;
using Ordering.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Query
{
    public class GetTranslationByIdQueryHandler : IRequestHandler<GetTranslationByIdQuery, Translation>, ITransientDependency
    {
        private readonly ITranslationRepository _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;

        public GetTranslationByIdQueryHandler(ITranslationRepository repository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Translation> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            Translation _translation = new Translation();

            _translation = await _repositoryRedis.GetAsync($"Translation_Order_{request.Id}");

            if (_translation == null)
            {
                _translation = await _repository.GetByIdAsync(cancellationToken, request.Id);
                await _repositoryRedis.UpdateAsync($"Translation_Order_{_translation.Id}", _translation);
            }

            return _translation;
        }
    }
}
