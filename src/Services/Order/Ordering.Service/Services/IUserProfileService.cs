using Ordering.Common.Models.User;
using Refit;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.Service.Services
{
    public interface IUserProfileService
    {
        [Post("/api/v1/UserProfiles/UpdateExpireTime")]
        Task UpdateExpireTimeUser(int userId, string time, string diettime, string tid);

        [Get("/api/v1/UserProfiles/{userId}")]
        Task<ApiResult<UserProfileDTO>> Get(int userId);
    }
}
