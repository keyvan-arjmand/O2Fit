using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Domain.Models;
using User.Service.v1.Command;
using WebFramework.Api;

namespace User.API.Controllers.v1
{
    [ApiVersion("1")]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("AdminExpireTimeUser")]
        public async Task<ApiResult> AdminExpireTimeUser(AdminExpireTimeUserDTO adminExpireTimeUserDTO)
        {
            await _mediator.Send(new AdminExpireTimeUserCommand
            {
                ExpireDate = adminExpireTimeUserDTO.ExpireDate,
                UserId = adminExpireTimeUserDTO.UserId,
            });
            return Ok();
        }
    }
}
