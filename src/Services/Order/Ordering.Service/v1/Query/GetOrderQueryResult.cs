using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query
{
    public class GetOrderQueryResult
    {
        public bool State { get; set; }
        public string TrackingCode { get; set; }
    }
}
