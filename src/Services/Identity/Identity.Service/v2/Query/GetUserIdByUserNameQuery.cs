using Identity.Service.v1.Query.GetUserInformation;
using MediatR;

namespace Identity.Service.v2.Query
{
    public class GetUserIdByUserNameQuery : IRequest<GetUserIdByUserNameViewModel>
    {
        public string UserName { get; set; }
    }
}
