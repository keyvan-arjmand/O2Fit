using Ordering.Domain.Models;
using Refit;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.Service.Services
{/// <summary>
/// Identity
/// </summary>
    public interface IUserService
    {
        [Post("/api/v1/Users/CheckReferreralInviter")]
        Task<ApiResult<string>> CheckUserReferreral(int userid, bool previousPurchase);

        [Post("/api/v1/Users/ApproveReferreral")]
        Task ApproveReferreral(int userId);

        [Post("/api/v1/Users/AddReferreralCount")]
        Task AddReferreralCount(int userId);

        [Get("/api/v1/Users/GetById")]
        Task<GlobalResult> GetUserById(int UserId, string grantpass);

        [Get("/api/v1/Users/GetIdByUserName")]
        Task<GlobalResult> GetUserByUserName(string userName);
    }




}
