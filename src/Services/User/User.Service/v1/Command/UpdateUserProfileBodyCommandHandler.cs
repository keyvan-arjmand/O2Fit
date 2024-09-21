using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileBodyCommandHandler : IRequestHandler<UpdateUserProfileBodyCommand, UserProfile>, IScopedDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public UpdateUserProfileBodyCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<UserProfile> Handle(UpdateUserProfileBodyCommand request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repository.TableNoTracking.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);

            if (_profile != null)
            {
                _profile.HeightSize = request.HeightSize;
                _profile.TargetHighHip = request.TargetHighHip;
                _profile.TargetArm = request.TargetArm;
                _profile.TargetBust = request.TargetBust;
                _profile.TargetHip = request.TargetHip;
                _profile.TargetShoulder = request.TargetShoulder;
                _profile.TargetWaist = request.TargetWaist;
                _profile.TargetWrist = request.TargetWrist;
                _profile.TargetThighSize = request.TargetThighSize;
                _profile.TargetNeckSize = request.TargetNeckSize;
                await _repository.UpdateAsync(_profile, cancellationToken);
                await _repositoryRedis.UpdateAsync($"UserProfile_{request.UserId}", _profile);
                _repository.Detach(_profile);
            }

            return _profile;
        }
    }
}
