using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class MelatPayment : BaseApi
    {
        public MelatGetApi data { get; set; }
    }

    public class MelatGetApi
    {
        public bool isError { get; set; }
        public string RefId { get; set; }
        public string ErrorMessage { get; set; }
        public string RedirectUrl { get; set; }
        public string PostUrl { get; set; }
    }
}
