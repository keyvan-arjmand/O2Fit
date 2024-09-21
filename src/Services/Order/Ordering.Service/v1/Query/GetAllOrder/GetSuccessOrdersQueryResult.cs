using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query.GetAllOrder
{
  public class GetSuccessOrdersQueryResult
    {
        public int Serial { get; set; }
        public string Username { get; set; }
        public double Amount { get; set; }
        public int Currency { get; set; }
        public string Package { get; set; }
        public DateTime CreateDate { get; set; }
        public string Bank { get; set; }
        public string NumberTracking { get; set; }//شماره پیگیری

    }
}
