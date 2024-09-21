using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class UsernameConfirm
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
