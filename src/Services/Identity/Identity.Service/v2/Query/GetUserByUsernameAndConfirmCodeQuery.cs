using MediatR;
using Service.Models;

namespace Identity.Service.v2.Query
{
    public class GetUserByUsernameAndConfirmCodeQuery : IRequest<UserConfirmDto>
    {
        public string Username { get; set; }
        public string Code { get; set; }
    }
}