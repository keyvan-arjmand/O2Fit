using Identity.Domain.UserDto;
using MediatR;

namespace Identity.Service.v1.Query
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public string UserName { get; set; }
    }
}
