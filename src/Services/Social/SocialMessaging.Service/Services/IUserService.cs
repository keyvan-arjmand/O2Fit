using Common;
using SocialMessaging.Domain.DTO;
using System.Threading.Tasks;

namespace SocialMessaging.Service.Services
{
    public interface IUserService : IScopedDependency
    {
        Task<UserProfileDTO> GetUserProfile(int UserId, string accessToken);
        Task<GetUserIdByUserName> GetUserIdByUserName(string userName, string accessToken);
    }
}
