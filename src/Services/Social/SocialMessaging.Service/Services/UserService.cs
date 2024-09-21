using Newtonsoft.Json;
using SocialMessaging.Domain.DTO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialMessaging.Service.Services
{
    public class UserService : IUserService
    {
        public async Task<UserProfileDTO> GetUserProfile(int UserId, string accessToken)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://idetitytest.o2fitt.com/api/");
            //client.BaseAddress = new Uri("https://localhost:44312/api/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var httpResponse = await client.GetAsync($"v1/Users/GetByUserIdAdmin?UserId={UserId}");

            var json = await httpResponse.Content.ReadAsStringAsync();
            GlobalResult res = JsonConvert.DeserializeObject<GlobalResult>(json);

            UserProfileDTO userProfile = JsonConvert.DeserializeObject<UserProfileDTO>(res.data.ToString());

            return userProfile;
        }

        public async Task<GetUserIdByUserName> GetUserIdByUserName(string userName, string accessToken)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://idetitytest.o2fitt.com/");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var comp = httpClient.GetAsync($"api/v2/Users/GetUserIdByUserName?userName={userName}").Result;

            var json = await comp.Content.ReadAsStringAsync();
            GlobalResult ResUsers = JsonConvert.DeserializeObject<GlobalResult>(json);
            if (ResUsers.data != null)
            {
                var userIdModel = JsonConvert.DeserializeObject<GetUserIdByUserName>(ResUsers.data.ToString());

                return userIdModel;
            }
            return null;
        }
    }
}
