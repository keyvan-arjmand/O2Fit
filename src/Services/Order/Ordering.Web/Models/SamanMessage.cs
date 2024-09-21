using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class SamanResultResponse : BaseApi
    {
        public SamanMessage data { get; set; }
    }

    public class SamanMessage
    {
        public bool isError { get; set; }
        public string errorMsg { get; set; }
        public string succeedMsg { get; set; }
    }
}
