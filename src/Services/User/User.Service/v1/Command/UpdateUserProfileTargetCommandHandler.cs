using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileTargetCommandHandler : IRequestHandler<UpdateUserProfileTargetCommand, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public UpdateUserProfileTargetCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<UserProfile> Handle(UpdateUserProfileTargetCommand request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repository.Table.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);
            _profile.TargetStep = request.TargetStep;
            _profile.TargetWeight = request.TargetWeight;
            _profile.WeightChangeRate = request.WeightChangeRate;
            _profile.DailyActivityRate = request.DailyActivityRate;

            await _repository.UpdateAsync(_profile,cancellationToken);
            await _repositoryRedis.UpdateAsync($"UserProfile_{request.UserId}",_profile);

            return _profile;
        }
    }
}