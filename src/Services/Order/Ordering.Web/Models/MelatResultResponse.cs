using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class MelatResultResponse : BaseApi
    {
        public MelatMessage data { get; set; }
    }
    public class MelatMessage
    {
        public bool isError { get; set; }
        public string errorMsg { get; set; }
        public string succeedMsg { get; set; }
    }
}
