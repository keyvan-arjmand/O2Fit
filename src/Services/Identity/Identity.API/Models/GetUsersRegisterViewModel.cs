using System;

namespace Identity.API.Models
{
    public class GetUsersRegisterDTO
    {
        public DateTime Day { get; set; }
        public int Count { get; set; }
    }

    public class DeviceInfoInLimitDateViewModel
    {

        public int googlePlay { get; set; }
        public int web { get; set; }
        public int appStore { get; set; }
        public int cafeBazar { get; set; }

    }

    public class ProfileCompleteViewModel
    {
        public int Count { get; set; }

    }

    public class GetUsersRegisterViewModel
    {
        public int Count { get; set; }
        public DateTime Day { get; set; }
    }

    public class UsersRegisterInfoInLimitDate
    {
        public int RegisterCount { get; set; }
        public int ActiveCount { get; set; }
        public DateTime Date { get; set; }


    }
}
