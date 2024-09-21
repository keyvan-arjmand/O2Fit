using MediatR;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Command
{
    public class UpdateMelatRefIdCommand : IRequest
    {
        public string RefId { get; set; }
        public Order Order { get; set; }
    }
}
