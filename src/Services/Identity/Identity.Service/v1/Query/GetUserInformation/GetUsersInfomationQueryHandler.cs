using Common;
using Data.Contracts;
using Domain;
using Identity.Domain.UserDto;
using Identity.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.v1.Query.GetUserInformation
{
    public class GetUsersInfomationQueryHandler : IRequestHandler<GetUsersInfomationQuery, GetUsersInformationQueryResult>, ITransientDependency
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepositoryRedis<UserProfileDTO> _repositoryRedis;
        private readonly IUserProfileService _UserProfileRefit;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetUsersInfomationQueryHandler(IUserRepository userRepository,
            IRepositoryRedis<UserProfileDTO> repositoryRedis, IUserProfileService UserProfileRefit,
            IRedisCacheClient redisCacheClient)
        {
            _userRepository = userRepository;
            _repositoryRedis = repositoryRedis;
            _UserProfileRefit = UserProfileRefit;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<GetUsersInformationQueryResult> Handle(GetUsersInfomationQuery request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.TableNoTracking
                .Where(u => u.Id == request.UserId).Include(c => c.Country)
                .ThenInclude(t => t.Translation).Select(s => new GetUsersInformation
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    Country = s.Country.Translation.Persian,
                    RegisterDate = s.RegisterDate,
                }).FirstAsync();



            GetUsersInformationQueryResult _user = new GetUsersInformationQueryResult();
            if (user != null)
            {

                var deviceInfo = await _redisCacheClient.Db8.GetAsync<DeviceInformationViewModel>($"DeviceInfo_{user.Id}");

                if (deviceInfo == null)
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new System.Uri("https://user.o2fitt.com");
                    //httpClient.BaseAddress = new System.Uri("https://localhost:44314");

                    var comp = await httpClient.GetAsync($"/api/v1/UserProfiles/GetDeviceInfoByUserId?userId={user.Id}");

                    deviceInfo = await _redisCacheClient.Db8.GetAsync<DeviceInformationViewModel>($"DeviceInfo_{user.Id}");

                }

                // UserProfileDTO CurrentUser = new UserProfileDTO();
                var CurrentUser = await _repositoryRedis.GetAsync($"UserProfile_{user.Id}");


                if (CurrentUser == null)
                {
                    var res = await _UserProfileRefit.UpdateRedisInformationUser(user.Id);

                    if (res.IsSuccessStatusCode)
                    {
                        CurrentUser = await _repositoryRedis.GetAsync($"UserProfile_{user.Id}");
                    }
                }

                _user.Id = user.Id;
                _user.UserName = user.UserName;
                _user.Country = user.Country;
                _user.RegisterDate = user.RegisterDate;

                _user.FullName = null;

                _user.ImageUri = null;
                _user.PkExpireDate = null;
                _user.OS = null;
                _user.AppVersion = null;
                _user.Market = null;
                _user.Brand = null;
                _user.PhoneModel = null;

                if (CurrentUser != null)
                {
                    _user.BirthDate = CurrentUser.BirthDate;
                    _user.FullName = CurrentUser.FullName;
                    _user.Gender = CurrentUser.Gender;
                    _user.ImageUri = CurrentUser.ImageUri;
                    _user.PkExpireDate = CurrentUser.PkExpireDate;

                }

                if (deviceInfo != null)
                {

                    _user.OS = deviceInfo.OS;
                    _user.AppVersion = deviceInfo.AppVersion;
                    _user.Market = deviceInfo.Market;
                    _user.Brand = deviceInfo.Brand;
                    _user.PhoneModel = deviceInfo.PhoneModel;
                }


            }

            return _user;
        }


    }

}

