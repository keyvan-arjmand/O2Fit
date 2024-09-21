using System;

namespace Service.Models
{
    public class UserConfirmDto
    {
        public string Username { get; set; }
        public string ConfirmCode { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}