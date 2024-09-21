using MediatR;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;
using Ordering.Domain.Enum;

namespace Ordering.Service.v1.Command
{
    public class CreateYekPayCommand : IRequest<string>
    {
        public bool IsDiscountActive { get; set; }
        public DiscountUser? DiscountUser { get; set; }
        public Order Order { get; set; }
    }
}
