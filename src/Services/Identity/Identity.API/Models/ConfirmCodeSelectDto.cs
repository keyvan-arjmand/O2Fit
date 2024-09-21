using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class ConfirmCodeSelectDto
    {
        public bool IsActive { get; set; }
        public bool WrongCode { get; set; }
        public bool ExpireCode { get; set; }
    }
}
