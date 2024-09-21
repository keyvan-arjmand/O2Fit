using System;
using System.ComponentModel.DataAnnotations;

namespace User.Domain.Models
{
    public class AdminExpireTimeUserDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
