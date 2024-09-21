using Blogging.Domain.Entities.Translation;
using Common;
using Data.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Command
{
    public class DeleteTranslationCommandHandler : IRequestHandler<DeleteTranslationCommand>, ITransientDependency
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
            var _values = _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id)).ToList();

            List<string> _keys = new List<string>();

            if (_values.Count > 0)
            {
                foreach (var item in _values)
                {
                    _keys.Add($"Translation_Blog_{item.Id}");
                }

                await _repositoryRedis.DeleteAllAsync(_keys);
            }
            await _repository.DeleteRangeAsync(_values, cancellationToken);
            return Unit.Value;
        }
    }
}
