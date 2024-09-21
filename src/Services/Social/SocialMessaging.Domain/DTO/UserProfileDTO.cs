

using System;

namespace SocialMessaging.Domain.DTO
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }

        public string? FullName { get; set; }
        public string? ImageUri { get; set; }
        public DateTime? PkExpireDate { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }


        public int? OS { get; set; }
        public string? Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Market { get; set; }
        public string? AppVersion { get; set; }

    }

    public class GetUserIdByUserName
    {
        public int UserId { get; set; }
    }
}
