using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.v1.Query
{
    public class GetReferreralInviterQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public bool PreviousPurchase { get; set; }
    }
}
