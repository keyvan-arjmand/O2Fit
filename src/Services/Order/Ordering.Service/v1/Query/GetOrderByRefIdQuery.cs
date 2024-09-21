using MediatR;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query
{
    public class GetOrderByRefIdQuery :IRequest<Order>
    {
        public string RefId { get; set; }
    }
}
