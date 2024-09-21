using Domain;
using MediatR;

namespace User.Service.v1.Query
{
    public class GetUserProfileQuery : IRequest<UserProfile>
    {
        public int UserId { get; set; }
    }
}