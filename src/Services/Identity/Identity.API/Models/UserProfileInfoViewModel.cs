using Identity.Domain.Enum;
using System;

namespace Identity.API.Models
{
    public class UserProfileInfoViewModel
    {
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime? PkExpireDate { get; set; }

    }

    public class UserProfileInfoViewModelResult
    {
        public UserProfileInfoViewModel? UserProfileInfoViewModel { get; set; }
        public DeviceInfoViewModel DeviceInfoViewModel { get; set; }
    }
    public class DeviceInfoViewModel
    {
        public int OS { get; set; }
        public string? Market { get; set; }
    }
}
