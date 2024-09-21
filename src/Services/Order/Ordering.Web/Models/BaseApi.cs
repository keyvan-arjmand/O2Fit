using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Web.Models
{
    public class BaseApi
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }
}
