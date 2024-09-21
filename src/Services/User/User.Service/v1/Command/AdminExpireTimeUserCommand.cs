using MediatR;
using System;

namespace User.Service.v1.Command
{
    public class AdminExpireTimeUserCommand : IRequest
    {
        public int UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
