using Common;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

            //_translation = await _repositoryRedis.GetAsync($"Translation_Food_{request.Id}");

            //if (_translation == null)
           // {
           _translation = await _repository.TableNoTracking.Where(t=>t.Id == request.Id)
               .FirstOrDefaultAsync(cancellationToken);
                await _repositoryRedis.UpdateAsync($"Translation_Food_{_translation.Id}", _translation);
           // }

            return _translation;
        }
    }
}
