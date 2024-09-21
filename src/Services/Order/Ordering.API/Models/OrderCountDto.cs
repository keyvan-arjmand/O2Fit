using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class OrderCountDto
    {
        public int OrderCount { get; set; }
        public int OrderSuccessCount { get; set; }
        public int OrderUnSuccessCount { get; set; }
    }
}
