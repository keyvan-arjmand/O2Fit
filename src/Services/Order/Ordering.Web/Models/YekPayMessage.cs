using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class YekPayMessage : BaseApi
    {
        public YekMessage data { get; set; }
    }

    public class YekMessage
    {

        public int OrderId { get; set; }
        public bool isError { get; set; }
        public string errorMsg { get; set; }
        public string succeedMsg { get; set; }
    }
}
