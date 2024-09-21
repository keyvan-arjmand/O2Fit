using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Identity.Service.v1.Command
{
    public class UpdateReferreralDiscountCommand :IRequest
    {
        public int userId { get; set; }
    }
}
