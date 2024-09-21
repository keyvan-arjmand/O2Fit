using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.YekPay
{
    public class YekPayMessage
    {
        public int OrderId { get; set; }
        public bool isError { get; set; }
        public string errorMsg { get; set; }
        public string succeedMsg { get; set; }
    }
}
