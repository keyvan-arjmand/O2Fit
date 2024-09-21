using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace User.Service.v1.Query
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public GetUserProfileQueryHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }
        public async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repositoryRedis.GetAsync($"UserProfile_{request.UserId}");
            
            if (_profile == null)
            {
                _profile = await _repository.TableNoTracking.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);
                await _repositoryRedis.UpdateAsync($"UserProfile_{request.UserId}", _profile);
            }

            return _profile;
        }
    }
}