using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Domain.Entities.Translation;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.v1.Query
{
    public class GetTranslationFullQueryHandler : IRequestHandler<GetTranslationFullQuery, List<Translation>>, ITransientDependency
    {
        private readonly IRepository<Translation> _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;

        public GetTranslationFullQueryHandler(IRepository<Translation> repository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<List<Translation>> Handle(GetTranslationFullQuery request, CancellationToken cancellationToken)
        {
            List<Translation> _GetTranslations = new List<Translation>();
            KeyValuePair<RedisKey, RedisValue>[] keyValuePairs = null;

            List<string> ListCheck = new List<string>();
            foreach (var item in request.Ids)
            {
                ListCheck.Add($"Translation_Order_{item}");
            }

            _GetTranslations = await _repositoryRedis.GetAllAsync(ListCheck);

            if (_GetTranslations.Count == 0)
            {
                _GetTranslations = await _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id)).ToListAsync();

                keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_GetTranslations.Count];

                for (int i = 0; i < _GetTranslations.Count; i++)
                {
                    string _json = JsonConvert.SerializeObject(_GetTranslations[i]);
                    keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"Translation_Order_{_GetTranslations[i].Id}", _json);
                }

                await _repositoryRedis.UpdateAllAsync(keyValuePairs);
            }
            else
            {
                int checkCount = _GetTranslations.Where(a => request.Ids.Contains(a.Id)).Count();

                if (checkCount != ListCheck.Count)
                {
                    _GetTranslations = await _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id)).ToListAsync();

                    keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_GetTranslations.Count];

                    for (int i = 0; i < _GetTranslations.Count; i++)
                    {
                        string _json = JsonConvert.SerializeObject(_GetTranslations[i]);
                        keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"Translation_Order_{_GetTranslations[i].Id}", _json);
                    }

                    await _repositoryRedis.UpdateAllAsync(keyValuePairs);

                }
            }

            return _GetTranslations;
        }
    }
}
