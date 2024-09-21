using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using System.Linq;
using User.Common.Utilities;

namespace User.Service.v1.Command
{
    public class UpdateTargetNutrientCommandHandler : IRequestHandler<UpdateTargetNutrientCommand, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public UpdateTargetNutrientCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<UserProfile> Handle(UpdateTargetNutrientCommand request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repository.TableNoTracking.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);
            _profile.TargetNutrient = StringConvertor.DoubleToString(request.TargetNutrient);

            await _repository.UpdateAsync(_profile, cancellationToken);

            await _repositoryRedis.UpdateAsync($"UserProfile_{request.UserId}", _profile);

            return _profile;
        }
    }
}