using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Identity.Domain.DeviceInformationDto;
using Identity.Domain.UserDto;

namespace Identity.Service.Services
{
    
    public interface IProfilesService :ITransientDependency
    {
        public Task<UserProfileDTO?> GetUserProfile(int id);

        public Task<DeviceInformationDto?> GetDeviceInformationUser(int userId);
    }
}
