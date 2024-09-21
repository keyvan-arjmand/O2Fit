using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class SamanPayment : BaseApi
    {
        public SamanGetApi data { get; set; }
    }

    public class SamanGetApi
    {
        public string token { get; set; }
        public string redirectUrl { get; set; }
        public string postUrl { get; set; }
    }
}
