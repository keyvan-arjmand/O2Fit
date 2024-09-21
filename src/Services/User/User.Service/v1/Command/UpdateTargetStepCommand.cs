using Domain;
using MediatR;

namespace User.Service.v1.Command
{
    public class UpdateTargetStepCommand : IRequest<UserProfile>
    {
        public int UserId { get; set; }
        public int TargetStep { get; set; }
    }
}