using Common;
using Data.Contracts;
using Domain;
using Identity.Common.Utilities;
using Identity.Domain.UserDto;
using Identity.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.v1.Query.GetUserInformation
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,
        PageResult<GetUsersInformationQueryResult>>, ITransientDependency
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepositoryRedis<UserProfileDTO> _repositoryRedis;
        private readonly IUserProfileService _userProfileRefit;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetUsersQueryHandler(IUserRepository userRepository,
            IRepositoryRedis<UserProfileDTO> repositoryRedis,
            IUserProfileService UserProfileRefit,
            IRedisCacheClient redisCacheClient)
        {
            _userRepository = userRepository;
            _repositoryRedis = repositoryRedis;
            _userProfileRefit = UserProfileRefit;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<PageResult<GetUsersInformationQueryResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<GetUsersInformationQueryResult> paging = new List<GetUsersInformationQueryResult>();
            List<GetUsersInformation> users = new List<GetUsersInformation>();
            int countDetails;

            if (request.StartDate != System.DateTime.MinValue && request.EndDate != System.DateTime.MinValue)
            {
                users = await _userRepository.TableNoTracking
                   .Include(c => c.Country)
                   .ThenInclude(t => t.Translation)
                   .Where(u => u.RegisterDate.Date >= request.StartDate.Date && u.RegisterDate <= request.EndDate.Date)
                   .OrderByDescending(u => u.Id)
                   .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                  .Take(request.PageSize)
                   .Select(s => new GetUsersInformation
                   {
                       Id = s.Id,
                       UserName = s.UserName,
                       Country = s.Country.Translation.Persian,
                       RegisterDate = s.RegisterDate

                   })
                                  .ToListAsync(cancellationToken);
                countDetails = users.Count();
            }
            else
            {
                if (request.Name != null)
                {

                    users = await _userRepository.TableNoTracking
                        .Include(c => c.Country)
                        .ThenInclude(t => t.Translation)
                        .Where(u => u.UserName.ToLower() == request.Name.ToLower())
                        .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                       .Take(request.PageSize)
                        .Select(s => new GetUsersInformation
                        {
                            Id = s.Id,
                            UserName = s.UserName,
                            Country = s.Country.Translation.Persian,
                            RegisterDate = s.RegisterDate

                        })
                                       .ToListAsync(cancellationToken);
                    countDetails = users.Count();
                }
                else
                {
                    users = await _userRepository.TableNoTracking
                        .Include(c => c.Country)
                        .ThenInclude(t => t.Translation)
                        .OrderByDescending(u => u.Id)
                        .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                      .Take(request.PageSize)
                        .Select(s => new GetUsersInformation
                        {
                            Id = s.Id,
                            UserName = s.UserName,
                            Country = s.Country.Translation.Persian,
                            RegisterDate = s.RegisterDate

                        }).ToListAsync(cancellationToken);
                    countDetails = users.Count();
                }
            }






            if (users.Count > 0)
            {
                foreach (var item in users)
                {

                    GetUsersInformationQueryResult invoicePaging = new GetUsersInformationQueryResult();

                    var deviceInfo = await _redisCacheClient.Db8.GetAsync<DeviceInformationViewModel>($"DeviceInfo_{item.Id}");

                    if (deviceInfo == null)
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.BaseAddress = new System.Uri("https://user.o2fitt.com");
                        //httpClient.BaseAddress = new System.Uri("https://localhost:44314");

                        var comp = await httpClient.GetAsync($"/api/v1/UserProfiles/GetDeviceInfoByUserId?userId={item.Id}");

                        deviceInfo = await _redisCacheClient.Db8.GetAsync<DeviceInformationViewModel>($"DeviceInfo_{item.Id}");

                    }

                    // UserProfileDTO CurrentUser = new UserProfileDTO();
                    var CurrentUser = await _repositoryRedis.GetAsync($"UserProfile_{item.Id}");


                    if (CurrentUser == null)
                    {
                        var res = await _userProfileRefit.UpdateRedisInformationUser(item.Id);

                        if (res.IsSuccessStatusCode)
                        {
                            CurrentUser = await _repositoryRedis.GetAsync($"UserProfile_{item.Id}");
                        }
                    }

                    invoicePaging.Id = item.Id;
                    invoicePaging.UserName = item.UserName;
                    invoicePaging.Country = item.Country;
                    invoicePaging.RegisterDate = item.RegisterDate;

                    invoicePaging.FullName = null;

                    invoicePaging.ImageUri = null;
                    invoicePaging.PkExpireDate = null;
                    invoicePaging.OS = null;
                    invoicePaging.AppVersion = null;
                    invoicePaging.Market = null;
                    invoicePaging.Brand = null;
                    invoicePaging.PhoneModel = null;

                    if (CurrentUser != null)
                    {
                        invoicePaging.BirthDate = CurrentUser.BirthDate;
                        invoicePaging.FullName = CurrentUser.FullName;
                        invoicePaging.Gender = CurrentUser.Gender;
                        invoicePaging.ImageUri = CurrentUser.ImageUri;
                        invoicePaging.PkExpireDate = CurrentUser.PkExpireDate;

                    }

                    if (deviceInfo != null)
                    {

                        invoicePaging.OS = deviceInfo.OS;
                        invoicePaging.AppVersion = deviceInfo.AppVersion;
                        invoicePaging.Market = deviceInfo.Market;
                        invoicePaging.Brand = deviceInfo.Brand;
                        invoicePaging.PhoneModel = deviceInfo.PhoneModel;
                    }

                    paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<GetUsersInformationQueryResult>
            {
                Count = countDetails,
                PageIndex = request.Page ?? 1,
                PageSize = request.PageSize,
                Items = paging
            };

            return result;
        }
    }
}