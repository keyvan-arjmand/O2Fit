using Domain;
using MediatR;

namespace User.Service.v1.Command
{
    public class UpdateUserTrackSpecificationCommand : IRequest<UserTrackSpecification>
    {
        public int UserId { get; set; }
        public UserTrackSpecification userTrackSpecification {get; set;}
    }
}