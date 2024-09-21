using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities.User;

namespace User.Service.v1.Command
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;
        private readonly IWebHostEnvironment _environment;
        private readonly IRepository<UserTrackSpecification> _repositoryTrack;
        private readonly IRepository<DeviceInformation> _deviceInformationRepository;
        private readonly IRedisCacheClient _redisCacheClient;
        public CreateUserProfileCommandHandler(IRepository<UserProfile> repository,
         IRepositoryRedis<UserProfile> repositoryRedis,
         IWebHostEnvironment environment,
         IRepository<UserTrackSpecification> repositoryTrack, IRepository<DeviceInformation> deviceInformationRepository, IRedisCacheClient redisCacheClient)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
            _environment = environment;
            _repositoryTrack = repositoryTrack;
            _deviceInformationRepository = deviceInformationRepository;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {

            if (await _redisCacheClient.Db10.ExistsAsync($"UserProfile_{request.UserProfile.UserId}"))
            {
                UserProfile result = await _redisCacheClient.Db10.GetAsync<UserProfile>
                    ($"UserProfile_{request.UserProfile.UserId}");

                if (!result.Equals(request.UserProfile))
                {
                    await _redisCacheClient.Db10.RemoveAsync($"UserProfile_{request.UserProfile.UserId}");
                    await _redisCacheClient.Db10.AddAsync($"UserProfile_{request.UserProfile.UserId}", request.UserProfile);
                    await _repository.UpdateAsync(request.UserProfile, cancellationToken);

                    var device = await _deviceInformationRepository.Table.FirstOrDefaultAsync(d => d.UserId == request.UserProfile.UserId, cancellationToken)
                        .ConfigureAwait(false);
                    if (device != null)
                    {
                        device.IsProfileComplete = true;
                        await _deviceInformationRepository.UpdateAsync(device, cancellationToken);
                    }
                    return request.UserProfile;
                }
                return result;
            }
            else
            {
                var age = DateTime.Now.Subtract(request.UserProfile.BirthDate).Days / 365;

                request.UserProfile.TargetNutrient = Formula.DefaultNutrients.DailyNutrients(age, request.UserProfile.Gender);
                request.UserProfile.CreateDate = DateTime.Now;

                await _repository.AddAsync(request.UserProfile, cancellationToken);
                await _redisCacheClient.Db10.AddAsync($"UserProfile_{request.UserProfile.UserId}", request.UserProfile);
                return request.UserProfile;
            }

            #region OldCode
            //UserProfile _profile = _repository.TableNoTracking.SingleOrDefault(a => a.UserId == request.UserProfile.UserId);

            //if (_profile == null)
            //{
            //    var age = DateTime.Now.Subtract(request.UserProfile.BirthDate).Days / 365;

            //    if (request.UserProfile.ImageUri != null)
            //    {
            //        var base64EncodedBytes = Convert.FromBase64String(request.UserProfile.ImageUri);

            //        string _Path = Path.Combine(_environment.WebRootPath, "UserProfile");

            //        DirectoryInfo destination;

            //        if (!Directory.Exists(_Path))
            //        {
            //            destination = Directory.CreateDirectory(_Path);
            //        }
            //        else
            //        {
            //            destination = new DirectoryInfo(_Path);
            //        }

            //        string _FileName = Guid.NewGuid().ToString() + ".jpg";

            //        var _Address = Path.Combine(_Path, _FileName);

            //        File.WriteAllBytes(_Address, base64EncodedBytes);

            //        request.UserProfile.ImageUri = _FileName;
            //    }

            //    request.UserProfile.TargetNutrient = await Formula.DefaultNutrients.DailyNutrients(age, request.UserProfile.Gender);

            //    _profile = request.UserProfile;
            //    _profile.CreateDate = DateTime.Now;

            //    _repository.Add(_profile);

            //    _repository.Detach(_profile);

            //    //UserTrackSpecification userTrackSpecification = new UserTrackSpecification()
            //    //{
            //    //    WaistSize = request.UserTrackSpecificationsWaistSize,
            //    //    WeightSize = request.UserTrackSpecificationsWeightSize,
            //    //    InsertDate =new DateTime(),
            //    //    UserProfileId = _profile.Id,
            //    //    _id = "0",
            //    //};

            //    //_repositoryTrack.Add(userTrackSpecification);

            //    await _repositoryRedis.UpdateAsync($"UserProfile_{_profile.UserId}", _profile);
            //}

            //_repository.Detach(_profile);

            //var device = await _deviceInformationRepository.Table
            //    .Where(d => d.UserId == _profile.UserId)
            //    .OrderByDescending(d => d.CreateDate).FirstOrDefaultAsync(cancellationToken);

            //if (device != null)
            //{
            //    device.IsProfileComplete = true;
            //    _deviceInformationRepository.Update(device);
            //}


            #endregion

        }
    }
}