using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class SamanSelectDto
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
        public string PostUrl { get; set; }
    }
}
