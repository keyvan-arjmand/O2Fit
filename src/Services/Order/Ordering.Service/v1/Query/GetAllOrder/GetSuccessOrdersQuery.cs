using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query.GetAllOrder
{
   public class GetSuccessOrdersQuery: IRequest<PageResult<GetSuccessOrdersQueryResult>>
    {
        public int PageSize { get; set; }
        public int? Page { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
