using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query.GetAllOrder
{
    public class GetOrderByUserIdQuery: IRequest<List<GetAllOrderQueryResult>>
    {
        public int UserId { get; set; }
     
    }
}
