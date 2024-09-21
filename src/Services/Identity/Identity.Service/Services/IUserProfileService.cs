using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Services
{
    public interface IUserProfileService
    {
        [Post("/api/v1/UserProfiles/UpdateReferreralExpireTime")]
        Task UpdateExpireTimeUser(int userId, string tid);


        [Get("/api/v1/UserProfiles/{userId}")]
        Task<HttpResponseMessage> UpdateRedisInformationUser(int userId);

    }
}
