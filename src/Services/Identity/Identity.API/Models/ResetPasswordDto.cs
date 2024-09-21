using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class ResetPasswordDto
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
