using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class GlobalResult
    {
        public object data { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
