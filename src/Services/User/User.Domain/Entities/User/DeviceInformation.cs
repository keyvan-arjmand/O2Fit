#nullable enable
using Domain;
using System;

namespace User.Domain.Entities.User
{
    public class DeviceInformation : BaseEntity
    {
        public int UserId { get; set; }
        public OsType OS { get; set; }
        public string Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Brightness { get; set; }
        public string? IPAddress { get; set; }
        public string? FontScale { get; set; }
        public string? Display { get; set; }
        public string? ApiLevelSdk { get; set; }
        public string? AndroidId { get; set; }
        public bool? IsTablet { get; set; }
        public bool? IsEmulator { get; set; }
        public string? AppVersion { get; set; }
        public string? Market { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsProfileComplete { get; set; }

        public bool IsPurchase { get; set; }


    }

    public enum OsType
    {
        Android = 0,
        Ios = 1,
        Web = 2
    }

}
