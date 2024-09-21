using Ordering.Domain.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.Data.Contracts
{
    public interface ISocialService
    {        
        [Post("/api/v1/ContactUs/SendMessageToUser")]
        [Headers("Authorization: Basic")]
        Task<ApiResult<ContactUsMessage>> SendMessageToUserAsync(AdminContactUsMessageDTO contactUsMessageDTO, string password, CancellationToken cancellationToken);
    }
}
