using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Command
{
    public class DeleteTranslationCommandHandler : IRequestHandler<DeleteTranslationCommand, Unit>, ITransientDependency
    {
        private readonly ITranslationRepository _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;

        public DeleteTranslationCommandHandler(ITranslationRepository translationRepository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = translationRepository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
        {
            var values = _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id)).ToList();

            List<string> keys = new List<string>();

            if (values.Count > 0)
            {
                foreach (var item in values)
                {
                    keys.Add($"Translation_Food_{item.Id}");
                }

                await _repositoryRedis.DeleteAllAsync(keys);
            }

            await _repository.DeleteRangeAsync(values, cancellationToken);

            return Unit.Value;
        }
    }
}
