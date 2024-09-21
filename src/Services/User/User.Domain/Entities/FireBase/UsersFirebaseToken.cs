using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.Domain.Entities.FireBase
{
   public class UsersFirebaseToken: BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public string FirebaseToken { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string AppVersion { get; set; }

        [Required]
        public string DeviceOS { get; set; }

        [Required]
        public string DeviceModel { get; set; }
    }
}
