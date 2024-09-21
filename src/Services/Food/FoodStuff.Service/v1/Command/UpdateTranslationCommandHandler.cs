using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using System;
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
            try
            {
                await _repositoryRedis.UpdateAsync($"Translation_Food_{request.Translation.Id}", request.Translation);
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
