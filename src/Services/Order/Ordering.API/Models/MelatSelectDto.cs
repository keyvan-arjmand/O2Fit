using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class MelatSelectDto
    {
        public bool isError { get; set; }
        public string RefId { get; set; }
        public string ErrorMessage { get; set; }
        public string RedirectUrl { get; set; }
        public string PostUrl { get; set; }
    }
}
