using System;
using System.Collections.Generic;
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
    public class UpdateUserTrackSpecificationCommandHandler : IRequestHandler<UpdateUserTrackSpecificationCommand, UserTrackSpecification>, ITransientDependency
    {
        private readonly IRepository<UserTrackSpecification> _repositoryTrack;
        private readonly IRepository<UserProfile> _repositoryUserProfile;
        private readonly IRepositoryRedis<List<UserTrackSpecification>> _repositoryRedisTrack;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedisUserProfile;

        public UpdateUserTrackSpecificationCommandHandler(
         IRepository<UserTrackSpecification> repositoryTrack,
         IRepository<UserProfile> repositoryUserProfile,
         IRepositoryRedis<List<UserTrackSpecification>> repositoryRedisTrack,
         IRepositoryRedis<UserProfile> repositoryRedisUserProfile)
        {
            _repositoryTrack = repositoryTrack;
            _repositoryUserProfile = repositoryUserProfile;
            _repositoryRedisTrack = repositoryRedisTrack;
            _repositoryRedisUserProfile = repositoryRedisUserProfile;
        }

        public async Task<UserTrackSpecification> Handle(UpdateUserTrackSpecificationCommand request, CancellationToken cancellationToken)
        {
            UserProfile userProfile = await _repositoryRedisUserProfile.GetAsync($"UserProfile_{request.UserId}");
            List<UserTrackSpecification> _track = await _repositoryRedisTrack.GetAsync($"UserTrackSpecification_{request.UserId}");
            UserTrackSpecification _value = new UserTrackSpecification();

            if (userProfile == null)
            {
                userProfile = await _repositoryUserProfile.TableNoTracking.FirstOrDefaultAsync(a => a.UserId == request.UserId,cancellationToken);
                await _repositoryRedisUserProfile.UpdateAsync($"UserProfile_{request.UserId}", userProfile);
            }

            if (_track == null)
            {
                _track = await _repositoryTrack.TableNoTracking
                                               .Where(a => a.UserProfileId == userProfile.Id)
                                               .OrderByDescending(a => a.InsertDate)
                                               .Take(30)
                                               .ToListAsync(cancellationToken);

                _value = _track.Where(a => a.InsertDate.Date == request.userTrackSpecification.InsertDate.Date).FirstOrDefault();
            }
            else
            {
                _value = _track.Where(a => a.InsertDate.Date == request.userTrackSpecification.InsertDate.Date).FirstOrDefault();
            }
            if (request.userTrackSpecification._id != null)
            {
                if (_value == null)
                {
                    _value = AddTrack(request.userTrackSpecification, userProfile.Id);
                    await _repositoryTrack.AddAsync(_value, cancellationToken);
                }
                else if (Convert.ToInt64(request.userTrackSpecification._id) >= Convert.ToInt64(_value._id))
                {
                    _value = UpdateTrack(_value, request.userTrackSpecification);
                    await _repositoryTrack.UpdateAsync(_value, cancellationToken);
                }

                List<UserTrackSpecification> _update = _UpdateValue(_track, _value);
                await _repositoryRedisTrack.UpdateAsync($"UserTrackSpecification_{request.UserId}", _update);
            }
            return _value;
        }

        public UserTrackSpecification AddTrack(UserTrackSpecification newValue, int id)
        {
            UserTrackSpecification _value = newValue;
            _value.UserProfileId = id;
            _value.InsertDate = newValue.InsertDate;
            return _value;
        }

        public UserTrackSpecification UpdateTrack(UserTrackSpecification oldValue, UserTrackSpecification newValue)
        {
            UserTrackSpecification _value = oldValue;
            _value.WeightSize = newValue.WeightSize > 0 ? newValue.WeightSize : _value.WeightSize;
            _value.BustSize = newValue.BustSize > 0 ? newValue.BustSize : _value.BustSize;
            _value.ArmSize = newValue.ArmSize > 0 ? newValue.ArmSize : _value.ArmSize;
            _value.WaistSize = newValue.WaistSize > 0 ? newValue.WaistSize : _value.WaistSize;
            _value.HighHipSize = newValue.HighHipSize > 0 ? newValue.HighHipSize : _value.HighHipSize;
            _value.HipSize = newValue.HipSize > 0 ? newValue.HipSize : _value.HipSize;
            _value.ThighSize = newValue.ThighSize > 0 ? newValue.ThighSize : _value.ThighSize;
            _value.NeckSize = newValue.NeckSize > 0 ? newValue.NeckSize : _value.NeckSize;
            _value.ShoulderSize = newValue.ShoulderSize > 0 ? newValue.ShoulderSize : _value.ShoulderSize;
            _value.WristSize = newValue.WristSize > 0 ? newValue.WristSize : _value.WristSize;
            _value._id = newValue._id;
            return _value;
        }

        public List<UserTrackSpecification> _UpdateValue(List<UserTrackSpecification> _list, UserTrackSpecification _new)
        {
            List<UserTrackSpecification> _tracking = _list;
            UserTrackSpecification _check = _tracking.Where(a => a.InsertDate.Date == _new.InsertDate.Date).FirstOrDefault();

            if (_check == null)
            {
                _tracking.Add(_new);
            }
            else
            {
                _tracking.Remove(_check);
                _tracking.Add(_new);
            }

            return _tracking.OrderByDescending(a => a.InsertDate).Take(30).ToList();
        }
    }
}