using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.DeviceInformationDto;
using Identity.Domain.Models;
using Identity.Domain.UserDto;
using Newtonsoft.Json;

namespace Identity.Service.Services
{
    public class ProfileService : IProfilesService
    {
        private string url = "https://user.o2fitt.com";
        public async Task<UserProfileDTO?>  GetUserProfile(int userId)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri($"{url}/api/v1/UserProfiles/{userId}");
            HttpResponseMessage response = await client.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            GlobalResult result = JsonConvert.DeserializeObject<GlobalResult>(json);

            if (result.isSuccess)
            {

                UserProfileDTO? userProfileDtoRes = JsonConvert.DeserializeObject<UserProfileDTO>(result.data.ToString() ?? string.Empty);

                return userProfileDtoRes;
            }

            return null;
        }

        public async Task<DeviceInformationDto?> GetDeviceInformationUser(int userId)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri($"{url}/api/v1/?userId={userId}");
            HttpResponseMessage response = await client.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            GlobalResult result = JsonConvert.DeserializeObject<GlobalResult>(json);

            DeviceInformationDto? deviceInfoDto = JsonConvert.DeserializeObject<DeviceInformationDto?>(result.data.ToString()!);
            

            return deviceInfoDto;
        }
    }
}
