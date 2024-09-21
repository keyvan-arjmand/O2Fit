using AutoMapper;
using Common;
using Common.Utilities;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.API.Models;
using User.Domain.Entities.User;
using User.Domain.Enum;
using User.Domain.Models;
using User.Service.v1.Command;
using User.Service.v1.Query;
using WebFramework.Api;

namespace User.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class UserProfilesController : BaseController
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepository<DeviceInformation> _deviceInfoRepository;
        private readonly IRepository<UserTrackSpecification> _repositoryTrackSpecification;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCacheClient;

        public UserProfilesController(IRepository<UserProfile> repository, IRepository<UserTrackSpecification> repositoryTrackSpecification,
           IConfiguration configuration, IMediator mediator, IMapper mapper, IRepository<DeviceInformation> deviceInfoRepository
           , IRedisCacheClient redisCacheClient)
        {
            _repository = repository; 
            _repositoryTrackSpecification = repositoryTrackSpecification;
            _configuration = configuration;
            _mediator = mediator;
            _mapper = mapper;
            _deviceInfoRepository = deviceInfoRepository;
            _redisCacheClient = redisCacheClient;
        }

        [HttpGet("CredibleUsersCount")]
        public int CredibleUsersCount()
        {
            return _repository.Table.Where(a => a.PkExpireDate != null).Count();
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult FoodHabits()
        {
            return Ok(EnumExtensions.GetEnumNameValues<FoodHabit>());
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult DailyActivityRates()
        {
            return Ok(EnumExtensions.GetEnumNameValues<DailyActivityRate>());
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ApiResult<UserProfileTrack>> GetUserTrackSpecification(int userId, CancellationToken cancellationToken)
        {
            UserProfileTrack userProfileTrack = new UserProfileTrack();
            UserProfileTrack _track = await _mediator.Send(new GetUserTrackSpecificationQuery { UserId = userId },cancellationToken);
            _track.TodayDate = DateTime.Now;
            _track.ForceUpdateVersions = _configuration.GetSection("ForceUpdateVersions").Value;
            _track.UserProfile.ImageUri = (_track.UserProfile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + _track.UserProfile.ImageUri;
            userProfileTrack = _track;
            return userProfileTrack;
        }

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="Image">Get Base64</param>
        /// <param name="FullName"></param>
        /// <returns></returns>
        [HttpPost("UpdateProfile")]
        public async Task<ApiResult<UserProfile>> UpdateProfile([FromForm] UpdateUserProfileDto input)
        {
            var userProfile = await _mediator.Send(new UpdateUserProfileCommand
            {
                UserId = input.UserId,
                FullName = input.FullName == "null" ? null : input.FullName,
                Image = input.Image,
                HeightSize = input.HeightSize,
                BirthDate = input.BirthDate,
                Gender = input.Gender,
                DailyActivityRate = input.DailyActivityRate,
                FoodHabit = input.FoodHabit
            });
            userProfile.ImageUri = (userProfile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + userProfile.ImageUri;

            return Ok(userProfile);
        }

        [HttpGet("{userId:int}")]
        public async Task<ApiResult<UserProfileDTO>> Get(int userId, CancellationToken cancellationToken)
        {
            // var _profile = await _repository.TableNoTracking.ProjectTo<UserProfileDTO>(_mapper.ConfigurationProvider)
            //     .SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            //UserProfile _check = await _repository.TableNoTracking.SingleOrDefaultAsync(u => u.UserId == userId);
            //var _valueCheck = UserProfileDTO.FromEntity(_mapper, _check);

            UserProfile _profile = await _mediator.Send(new GetUserProfileQuery { UserId = userId });
            UserProfileDTO profile = UserProfileDTO.FromEntity(_mapper, _profile);
            profile.TodayDate = DateTime.Now.Date;
            profile.ForceUpdateVersions = _configuration.GetSection("ForceUpdateVersions").Value;
            profile.ImageUri = (profile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + profile.ImageUri;
            return profile;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register(UserProfileRegisterDto userProfileRegisterDto, CancellationToken cancellationToken)
        {
            try
            {
                UserProfile _profile = await _mediator.Send(new CreateUserProfileCommand
                {
                    UserProfile = userProfileRegisterDto.ToEntity(_mapper),
                    //UserTrackSpecificationsWaistSize = userProfileRegisterDto.UserTrackSpecificationsWaistSize,
                    //UserTrackSpecificationsWeightSize = userProfileRegisterDto.UserTrackSpecificationsWeightSize
                });
                return Ok(_profile);
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPut]
        public async Task<ApiResult<UserProfile>> UpdateTargetNutrient(TargetNutrientDto targetNutrientDto, CancellationToken cancellationToken)
        {
            var userProfile = await _mediator.Send(new UpdateTargetNutrientCommand { UserId = targetNutrientDto.UserId, TargetNutrient = targetNutrientDto.TargetNutrient });
            //var targetNutrient = new TargetNutrientDto()
            //{
            //    UserId = userProfile.UserId,
            //    TargetNutrient = StringConvertor.ToNumber(userProfile.TargetNutrient)
            //};
            userProfile.ImageUri = (userProfile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + userProfile.ImageUri;
            return userProfile;
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<ApiResult<UserTrackSpecification>> UpdateUserTrackSpecification(int userId, UserTrackSpecificationDto userTrackSpecificationDto, CancellationToken cancellationToken)
        {
            var userTrackSpecification = userTrackSpecificationDto.ToEntity(_mapper);
            userTrackSpecification._id = userTrackSpecificationDto._id;
            return await _mediator.Send(new UpdateUserTrackSpecificationCommand
            {
                UserId = userId,
                userTrackSpecification = userTrackSpecification
            },cancellationToken);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<ApiResult<UserProfile>> UpdateTargetStep(TargetStepDto targetStepDto)
        {
            return await _mediator.Send(new UpdateTargetStepCommand
            {
                UserId = targetStepDto.UserId,
                TargetStep = targetStepDto.TargetStep
            });
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<ApiResult<UserProfile>> UpdateTarget(UserProfileTargetDto userProfileTargetDto)
        {
            userProfileTargetDto.TargetStep = (userProfileTargetDto.TargetStep < 1 || userProfileTargetDto.TargetStep == null) ? 5000 : userProfileTargetDto.TargetStep;
            var userProfile = await _mediator.Send(new UpdateUserProfileTargetCommand
            {
                UserId = userProfileTargetDto.UserId,
                TargetWeight = userProfileTargetDto.TargetWeight,
                WeightChangeRate = userProfileTargetDto.WeightChangeRate,
                TargetStep = userProfileTargetDto.TargetStep,
                DailyActivityRate = userProfileTargetDto.DailyActivityRate
            });
            userProfile.ImageUri = (userProfile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + userProfile.ImageUri;
            return userProfile;
        }

        [HttpPut("UpdateBodyShapes")]
        public async Task<ApiResult<UserProfile>> UpdateBodyShapes(UserProfileBodyDto userProfileBodyDto)
        {
            var userProfile = await _mediator.Send(new UpdateUserProfileBodyCommand
            {
                UserId = userProfileBodyDto.UserId,
                HeightSize = userProfileBodyDto.HeightSize,
                TargetHighHip = userProfileBodyDto.TargetHighHip,
                TargetArm = userProfileBodyDto.TargetArm,
                TargetBust = userProfileBodyDto.TargetBust,
                TargetHip = userProfileBodyDto.TargetHip,
                TargetShoulder = userProfileBodyDto.TargetShoulder,
                TargetWaist = userProfileBodyDto.TargetWaist,
                TargetWrist = userProfileBodyDto.TargetWrist,
                TargetNeckSize = userProfileBodyDto.TargetNeckSize,
                TargetThighSize = userProfileBodyDto.TargetThighSize
            });
            userProfile.ImageUri = (userProfile.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "UserProfile/" + userProfile.ImageUri;
            return userProfile;
        }

        [Route("UserSpecificationHistory")]
        [HttpGet]
        public async Task<ApiResult<List<UserTrackSpecification>>> GetUserSpecificationHistory(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            //var userTrackSpecifications = await _repositoryTrackSpecification.Table.Where(s => s.InsertDate >= dateTime && s.UserProfiles.UserId == userId).ToListAsync(cancellationToken);
            var userTrackSpecifications = await (from userSpecifications in _repositoryTrackSpecification.Table
                                                 .Include(s => s.UserProfiles)
                                                 where (userSpecifications.InsertDate >= dateTime) && (userSpecifications.UserProfiles.UserId == userId)
                                                 select userSpecifications).ToListAsync(cancellationToken);
            return userTrackSpecifications;
        }

        [Route("UpdateExpireTime")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult> UpdateExpireTime(int userId, string time, string tid, string diettime)
        {
            await _mediator.Send(new UpdateExpireTimeCommand
            {
                UserId = userId,
                Time = time,
                DietTime = diettime,
                Tid = tid,
            });

            return Ok();
        }

        [Route("UpdateReferreralExpireTime")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult> UpdateReferreralExpireTime(int userId, string tid)
        {
            try
            {
                await _mediator.Send(new UpdateReferreralExpireTimeCommand
                {
                    UserId = userId,
                    Tid = tid
                });
            }
            catch (Exception ex)
            {

                return new ApiResult(false, ApiResultStatusCode.BadRequest, ex.Message);
            }

            return Ok();
        }


        [Route("GetUsersGenderCount")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<UserCount>> GetUsersGenderCount(CancellationToken cancellationToken)
        {
            var result = new UserCount();
            result.females = await (from userProfile in _repository.TableNoTracking
                           .Where(u => u.Gender == Gender.Female)
                                    select userProfile).CountAsync(cancellationToken);
            result.males = await (from userProfile in _repository.TableNoTracking
                            .Where(u => u.Gender == Gender.Male)
                                  select userProfile).CountAsync(cancellationToken);
            return result;
        }

        [Route("GetTotalUsersLoseWeight")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<double>> GetTotalUsersLoseWeight(CancellationToken cancellationToken)
        {
            try
            {
                double totalLoseWeight = 0;
                double totalGaineWeight = 0;
                var userIds = await _repositoryTrackSpecification.Table
                        .Include(s => s.UserProfiles)
                        .Where(u => u.WeightSize > 35 && u.WeightSize < 150)
                        .Select(u => u.UserProfiles.UserId).ToListAsync(cancellationToken);
                userIds = userIds.Distinct().ToList();
                foreach (var userId in userIds)
                {
                    List<ValueInDate> userWeights = await _repositoryTrackSpecification.Table
                        .Include(s => s.UserProfiles)
                        .Where(u => u.UserProfiles.UserId == userId && u.WeightSize > 35 && u.WeightSize < 150)
                        .OrderBy(s => s.InsertDate)
                        .Select(s => new ValueInDate
                        {
                            dateTime = s.InsertDate,
                            value = s.WeightSize ?? 0
                        }).ToListAsync(cancellationToken);

                    double weight = userWeights.First().value - userWeights.Last().value;
                    if (weight > 0)
                    {
                        totalLoseWeight = totalLoseWeight + weight;
                    }
                    else
                    {
                        totalGaineWeight = totalGaineWeight + weight * (-1);
                    }
                }

                return new List<double>() {
                totalLoseWeight,totalGaineWeight
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [Route("AddDeviceInfo")]
        [HttpPost]
        public async Task<ApiResult> AddDeviceInfo(DeviceInformationDto deviceInformationDto, CancellationToken cancellationToken)
        {
            DeviceInformation deviceInformation = deviceInformationDto.ToEntity(_mapper);

            if (await _redisCacheClient.Db8.ExistsAsync($"DeviceInformation_{deviceInformation.UserId}"))
            {
                DeviceInformation result = await _redisCacheClient.Db10.GetAsync<DeviceInformation>
                    ($"DeviceInformation_{deviceInformation.UserId}");

                if (!result.Equals(deviceInformation))
                {
                    deviceInformation.CreateDate = DateTime.Now;
                    await _redisCacheClient.Db8.RemoveAsync($"DeviceInformation_{deviceInformation.UserId}");
                    await _redisCacheClient.Db8.AddAsync($"DeviceInformation_{deviceInformation.UserId}", deviceInformation);
                    await _deviceInfoRepository.UpdateAsync(deviceInformation, cancellationToken);
                }
                return Ok();
            }
            deviceInformation.CreateDate = DateTime.Now;
            await _deviceInfoRepository.AddAsync(deviceInformation, cancellationToken);
            await _redisCacheClient.Db8.AddAsync($"DeviceInformation_{deviceInformation.UserId}", deviceInformation);
            return Ok();
        }


        [Route("GetDeviceInfoByUserId")]
        [HttpGet]
        public async Task<ApiResult<DeviceInformationViewModel>> GetDeviceInfoByUserId(int userId, CancellationToken cancellationToken)
        {
            var deviceInfo = await _deviceInfoRepository
                .TableNoTracking
                .Where(d => d.UserId == userId)
                .OrderByDescending(s => s.CreateDate)
                .Select(s => new DeviceInformationViewModel
                {
                    OS = (int)s.OS,
                    Market = s.Market,
                    AppVersion = s.AppVersion,
                    Brand = s.Brand,
                    PhoneModel = s.PhoneModel
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (deviceInfo != null)
            {
                await _redisCacheClient.Db8.AddAsync($"DeviceInfo_{userId}", deviceInfo);
                return Ok();
            }

            return Ok();
        }

        [Route("GetUserProfile")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<UserProfileViewModel> GetUserProfile(int userId, CancellationToken cancellationToken)
        {
            var userProfile = await _repository.TableNoTracking.FirstAsync(u => u.UserId == userId, cancellationToken);

            var userTrack = await _repositoryTrackSpecification.TableNoTracking
                .Where(ut => ut.UserProfileId == userProfile.Id).OrderByDescending(ut => ut.Id).ToListAsync(cancellationToken);

            var deviceInfo = await _deviceInfoRepository.TableNoTracking.Where(d => d.UserId == userId)
                .Select(s => new DeviceInformationViewModel
                {
                    AppVersion = s.AppVersion,
                    Brand = s.Brand,
                    Id = s.Id,
                    Market = s.Market,
                    OS = (int)s.OS,
                    PhoneModel = s.PhoneModel,
                }).OrderByDescending(d => d.Id).ToListAsync(cancellationToken);



            return new UserProfileViewModel
            {
                UserProfile = userProfile,
                UserTrackSpecification = userTrack == null ? null : userTrack.First(),
                DeviceInformationViewModel = deviceInfo.First(),
            };


        }

        [Route("GetUserPkExpireDate")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<UserPkExpireDateViewModel> GetUserPkExpireDate(int userId)
        {
            var result = await _repository.TableNoTracking.Where(u => u.UserId == userId)
                .Select(s => new UserPkExpireDateViewModel
                {
                    PkExpireDate = s.PkExpireDate
                }).FirstAsync();

            return result;
        }



        [Route("ProfileCompleteInLimitDate")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public int ProfileCompleteInLimitDate(DateTime dateTimeDTO)
        {

            var profileDTO = _repository.TableNoTracking
                .Count(u => u.CreateDate.Date == dateTimeDTO);


            return profileDTO;

        }

        [Route("GetUserProfileByUserId")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<UserProfileInfoViewModelResult> GetUserProfileByUserId(int userId, CancellationToken cancellationToken)
        {

            UserProfileInfoViewModel userProfileInfoViewModel = await _repository.TableNoTracking
                .Where(u => u.UserId == userId).Select(u => new UserProfileInfoViewModel
                {
                    BirthDate = u.BirthDate,
                    Gender = u.Gender,
                    PkExpireDate = u.PkExpireDate
                }).FirstOrDefaultAsync(cancellationToken);

            var deviceInfo = await _deviceInfoRepository.TableNoTracking.Where(d => d.UserId == userId)
                .OrderByDescending(d => d.Id)
                          .Select(s => new DeviceInfoViewModel
                          {
                              Market = s.Market,
                              OS = (int)s.OS,
                          }).ToListAsync(cancellationToken);

            var result = new UserProfileInfoViewModelResult
            {
                DeviceInfoViewModel = deviceInfo == null ? null : deviceInfo.FirstOrDefault(),
                UserProfileInfoViewModel = userProfileInfoViewModel
            };

            return result;

        }

        [Route("UpdateFastingMode")]
        [HttpPut]
        [Authorize]
        public async Task<ApiResult> UpdateFastingMode(UpdateFastingModeDTO updateFastingModeDTO, CancellationToken cancellationToken)
        {
            UserProfile userProfile = await _repository.Table
                .Where(p => p.UserId == updateFastingModeDTO.UserId).FirstOrDefaultAsync();

            if (userProfile != null)
            {
                userProfile.FastingMode = updateFastingModeDTO.FastingMode;
                await _repository.UpdateAsync(userProfile, cancellationToken);
            }

            return new ApiResult(true, ApiResultStatusCode.Success);

        }
    }
}
