using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class ForgotPasswordChangeSelectDto
    {
        public bool IsChange { get; set; }
        public bool ExpireCode { get; set; }

    }
}
