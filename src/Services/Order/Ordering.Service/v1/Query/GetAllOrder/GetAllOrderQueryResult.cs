using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query.GetAllOrder
{
   public class GetAllOrderQueryResult
    {
        public int OrderId { get; set;}
        public string Username { get; set; }
        public double Amount { get; set; }
        public string Package { get; set; }
        public string SecurePan { get; set; }//شماره کارت 
        public int Currency { get; set; }
        public int BankId { get; set; }
        public DateTime CreateDate { get; set; }
        public string State { get; set; }
        public DateTime ExpireTime { get; set; }//banktransaction
        public DateTime RegisterDateUser { get; set; }
    }
}
