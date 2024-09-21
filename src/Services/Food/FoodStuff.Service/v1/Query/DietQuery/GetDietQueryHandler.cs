using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.DietQuery
{
    public class GetDietQueryHandler : IRequestHandler<GetDietQuery, List<UserTrackDietPack>>, IScopedDependency
    {
        private readonly IRepository<UserTrackDietPack> _userTrackDietrepository;
        private readonly IRepositoryRedis<List<UserTrackDietPack>> _repositoryRedis;
        public GetDietQueryHandler(IRepository<UserTrackDietPack> userTrackDietrepository, IRepositoryRedis<List<UserTrackDietPack>> repositoryRedis)
        {
            _repositoryRedis = repositoryRedis;
            _userTrackDietrepository = userTrackDietrepository;
        }
        public async Task<List<UserTrackDietPack>> Handle(GetDietQuery request, CancellationToken cancellationToken)
        {
            List<UserTrackDietPack> _ListUserTrack = new List<UserTrackDietPack>();

            _ListUserTrack = await _repositoryRedis.GetAsync($"Diet_{request.UserId}");

            if (_ListUserTrack == null)
            { 
                _ListUserTrack = await _userTrackDietrepository.Table
                                                       .Where(a => a.UserId == request.UserId)
                                                       .ToListAsync(cancellationToken);

                if (_ListUserTrack.Count > 0)
                {
                    foreach (var item in _ListUserTrack)
                    {
                        _userTrackDietrepository.Detach(item);
                    }
                }

                await _repositoryRedis.UpdateAsync($"Diet_{request.UserId}", _ListUserTrack);
            }

            return _ListUserTrack;
        }
    }
}
