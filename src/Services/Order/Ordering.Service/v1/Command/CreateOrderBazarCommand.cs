using MediatR;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Command
{
   public class CreateOrderBazarCommand : IRequest<Order>
    {
      
        public CafeBazarModel cafeBazarModel { get; set; }
    }
}
