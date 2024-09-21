using Data.Contracts;
using Identity.API.Models;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Identity.API.Controllers.v2
{
    [ApiVersion("2")]
    public class AdminController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetUserRegistrationStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResultUserRegisterStatus<GetRegisterStatusUsersDTO>> GetUserRegistrationStatus(int? page, int pageSize)
        {
            List<GetRegisterStatusUsersDTO> UserRegisterStatus = await _userRepository
                .TableNoTracking.OrderByDescending(u => u.Id).Select(c =>
                    new GetRegisterStatusUsersDTO
                    {
                        IsActive = c.IsActive,
                        RegisterDate = c.RegisterDate,
                        UserId = c.Id
                    }).Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToListAsync();



            int ActiveCount = UserRegisterStatus.Count(u => u.IsActive);
            int RegisterCount = UserRegisterStatus.Count();
            var token = await HttpContext.GetTokenAsync("access_token");
            foreach (var item in UserRegisterStatus)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://usertest.o2fitt.com/api/");
                //client.BaseAddress = new Uri("https://localhost:44314/api/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var comp = await client.GetAsync($"v1/UserProfiles/GetUserProfileByUserId?userId={item.UserId}");
                var json = await comp.Content.ReadAsStringAsync();
                GlobalResult ResUsers = JsonConvert.DeserializeObject<GlobalResult>(json);
                UserProfileInfoViewModelResult result = JsonConvert.DeserializeObject<UserProfileInfoViewModelResult>(ResUsers.data.ToString());

                item.UserProfileInfoViewModelResult = result;
            }

            return new PageResultUserRegisterStatus<GetRegisterStatusUsersDTO>
            {
                Count = UserRegisterStatus.Count,
                Items = UserRegisterStatus,
                PageSize = pageSize,
                ActiveCount = ActiveCount,
                RegisterCount = RegisterCount,

            };
        }
    }
}
