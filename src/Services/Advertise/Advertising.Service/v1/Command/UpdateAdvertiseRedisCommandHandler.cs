using Advertising.Domain.Entities.Advertise;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Service.v1.Command
{
    public class UpdateAdvertiseRedisCommandHandler : IRequestHandler<UpdateAdvertiseRedisCommand>, IScopedDependency
    {
        private readonly IRepository<Advertise> _repository;
        private readonly IRepositoryRedis<Advertise> _repositoryRedis;

        public UpdateAdvertiseRedisCommandHandler(IRepository<Advertise> repository, IRepositoryRedis<Advertise> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(UpdateAdvertiseRedisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Advertise> _ads = await _repository.TableNoTracking.Where(a => a.IsActive == true).ToListAsync(cancellationToken);

                List<string> _keys = new List<string>();

                if (_ads.Count > 0)
                {
                    foreach (var item in _ads)
                    {
                        _keys.Add($"Ads_{item.Id}");
                        _repository.Detach(item);
                    }
                }

                List<Advertise> _getRedisAds = await _repositoryRedis.GetAllAsync(_keys);

                foreach (var item in _getRedisAds)
                {
                    item.AdvertiseCountries = null;
                }

                await _repository.UpdateRangeAsync(_getRedisAds, cancellationToken);

                if (_getRedisAds.Count > 0)
                {
                    foreach (var item in _getRedisAds)
                    {
                        _repository.Detach(item);
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return Unit.Value;
        }
    }
}
