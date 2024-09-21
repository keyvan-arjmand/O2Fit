using Blogging.Domain.Entities.Translation;
using Common;
using Data.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            return _translation;
            
        }
    }
}
