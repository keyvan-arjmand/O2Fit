using Advertising.Domain.Entities.Advertise;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Service.v1.Command
{
    public class UpdateAdvertiseByIdCommandHandler : IRequestHandler<UpdateAdvertiseByIdCommand>, IScopedDependency
    {
        private readonly IRepository<Advertise> _repository;
        private readonly IRepository<AdvertiseCountry> _repositoryCounrey;
        private readonly IRepositoryRedis<Advertise> _repositoryRedis;
        private readonly IRepositoryRedis<List<Advertise>> _repositoryRedisListAds;

        public UpdateAdvertiseByIdCommandHandler(IRepository<Advertise> repository,
                                                 IRepository<AdvertiseCountry> repositoryCounrey,
                                                 IRepositoryRedis<Advertise> repositoryRedis,
                                                 IRepositoryRedis<List<Advertise>> repositoryRedisListAds)
        {
            _repository = repository;
            _repositoryCounrey = repositoryCounrey;
            _repositoryRedis = repositoryRedis;
            _repositoryRedisListAds = repositoryRedisListAds;
        }

        public async Task<Unit> Handle(UpdateAdvertiseByIdCommand request, CancellationToken cancellationToken)
        {
            Advertise _ads = new Advertise();
            KeyValuePair<RedisKey, RedisValue>[] keyValuePairs = null;

            _ads = await _repositoryRedis.GetAsync($"Ads_{request.Id}");

            if (_ads == null)
            {
                _ads = await _repository.Table.Include(a => a.AdvertiseCountries)
                                              .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
                if(_ads == null)
                {
                    return Unit.Value;
                }

                _repository.Detach(_ads);
            }
            else if(_ads.IsActive == false)
            {
                return Unit.Value;
            }

            _ads = Services.CalculateAds.Calulate(_ads, request.Click, request.View, request.Hint);

            if (_ads.IsActive == false)
            {
                List<AdvertiseCountry> _adsCountry = await _repositoryCounrey.Table.Include(a => a.Advertise).Where(a => a.AdvertiseId == _ads.Id).ToListAsync(cancellationToken);

                if (_adsCountry != null)
                {

                    List<string> _values = new List<string>();

                    foreach (var item in _adsCountry)
                    {
                        _values.Add($"Ads_Country_{item.CountryId}");
                        _repositoryCounrey.Detach(item);
                    }

                    List<List<Advertise>> _cache = await _repositoryRedisListAds.GetAllWithNullAsync(_values);

                    List<List<Advertise>> _get = new List<List<Advertise>>();
                    List<string> _Key = new List<string>();

                    for (int i = 0; i < _cache.Count; i++)
                    {
                        if (_cache[i] != null)
                        {
                            _get.Add(_cache[i]);
                            _Key.Add(_values[i]);
                        }
                    }

                    keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_get.Count];

                    for (int i = 0; i < _get.Count; i++)
                    {
                        Advertise advertises = _get[i].Where(a => a.Id == _ads.Id).FirstOrDefault();

                        if (advertises != null)
                        {
                            _get[i].Remove(advertises);

                            string _json = JsonConvert.SerializeObject(_get[i], new JsonSerializerSettings()
                            {
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                Formatting = Formatting.Indented
                            });

                            keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"{_Key[i]}", _json);
                        }
                    }

                    await _repositoryRedisListAds.UpdateAllAsync(keyValuePairs);
                }

            }

            await _repositoryRedis.UpdateDisableLoopAsync($"Ads_{request.Id}", _ads);

            return Unit.Value;
        }
    }
}
