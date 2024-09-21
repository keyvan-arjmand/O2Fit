using Domain;
using MediatR;

namespace Identity.Service.v1.Query
{
    public class GetUserConfirmQuery : IRequest<User>
    {
        public string UserName { get; set; }
    }
}
