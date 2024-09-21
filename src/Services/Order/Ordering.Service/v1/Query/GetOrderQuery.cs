using MediatR;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int Id { get; set; }
    }
}
