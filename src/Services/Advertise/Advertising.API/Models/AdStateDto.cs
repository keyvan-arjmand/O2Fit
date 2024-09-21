using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertising.API.Models
{
    public class AdStateDto
    {
        public int Id { get; set; }
        public bool Click { get; set; }
        public bool View { get; set; }
        public bool Hint { get; set; }
    }
}
