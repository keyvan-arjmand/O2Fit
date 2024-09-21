using System;
using User.Domain.Enum;

namespace User.API.Models
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
}
