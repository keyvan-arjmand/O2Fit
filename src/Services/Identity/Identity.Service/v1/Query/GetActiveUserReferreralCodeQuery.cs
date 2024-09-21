using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Identity.Service.v1.Query
{
    public class GetActiveUserReferreralCodeQuery : IRequest<int>
    {
        public int UserId { get; set; }
    }
}
