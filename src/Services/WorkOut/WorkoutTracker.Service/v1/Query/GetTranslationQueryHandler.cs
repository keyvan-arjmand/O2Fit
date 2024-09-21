using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Repositories;
using WorkoutTracker.Domain.Entities.Translation;

namespace Service.v1.Query
{
    public class GetTranslationQueryHandler : IRequestHandler<GetTranslationQuery, List<SelectOption<int>>>, ITransientDependency
    {
        private readonly IRepository<Translation> _repository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;

        public GetTranslationQueryHandler(IRepository<Translation> repository, IRepositoryRedis<Translation> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<List<SelectOption<int>>> Handle(GetTranslationQuery request, CancellationToken cancellationToken)
        {
            List<SelectOption<int>> _selected = new List<SelectOption<int>>();
            List<Translation> _GetTranslations = new List<Translation>();
            KeyValuePair<RedisKey, RedisValue>[] keyValuePairs = null;

            List<string> ListCheck = new List<string>();
            foreach (var item in request.Ids)
            {
                ListCheck.Add($"Translation_Workout_{item}");
            }

            _GetTranslations = await _repositoryRedis.GetAllAsync(ListCheck);

            if (_GetTranslations.Count == 0)
            {
                _GetTranslations = await _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id)).ToListAsync();

                keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_GetTranslations.Count];

                for (int i = 0; i < _GetTranslations.Count; i++)
                {
                    string _json = JsonConvert.SerializeObject(_GetTranslations[i]);
                    keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"Translation_Workout_{_GetTranslations[i].Id}", _json);
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
                        keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"Translation_Workout_{_GetTranslations[i].Id}", _json);
                    }

                    await _repositoryRedis.UpdateAllAsync(keyValuePairs);

                }
            }

            if (_GetTranslations.Count > 0)
            {
                foreach (var item in _GetTranslations)
                {
                    string _LanguageValue = null;

                    if (request.Language == null)
                    {
                        _LanguageValue = item.Persian;
                    }
                    else
                    {
                        switch (request.Language)
                        {
                            case "Persian":
                                {
                                    _LanguageValue = item.Persian;
                                    break;
                                }
                            case "English":
                                {
                                    _LanguageValue = item.English;
                                    break;
                                }
                            case "Arabic":
                                {
                                    _LanguageValue = item.Arabic;
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    SelectOption<int> _translate = new SelectOption<int>()
                    {
                        Value = item.Id,
                        Text = _LanguageValue
                    };

                    _selected.Add(_translate);
                }
            }

            return _selected;
        }


    }
}
