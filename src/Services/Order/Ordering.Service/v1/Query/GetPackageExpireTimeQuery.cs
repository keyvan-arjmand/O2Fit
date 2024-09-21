using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query
{
    public class GetPackageExpireTimeQuery : IRequest<string>
    {
        public int UserId { get; set; }
    }
}
