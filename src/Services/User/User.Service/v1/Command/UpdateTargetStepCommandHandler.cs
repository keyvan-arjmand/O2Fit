using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common;
using Data.Contracts;
using Domain;
using MediatR;

namespace User.Service.v1.Command
{
    public class UpdateTargetStepCommandHandler : IRequestHandler<UpdateTargetStepCommand, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public UpdateTargetStepCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<UserProfile> Handle(UpdateTargetStepCommand request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repository.TableNoTracking.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync();

            if (_profile != null)
            {
                _profile.TargetStep = request.TargetStep;
                await _repository.UpdateAsync(_profile, cancellationToken);
                await _repositoryRedis.UpdateAsync($"UserProfile_{request.UserId}", _profile);

                _repository.Detach(_profile);
            }

            return _profile;
        }
    }
}