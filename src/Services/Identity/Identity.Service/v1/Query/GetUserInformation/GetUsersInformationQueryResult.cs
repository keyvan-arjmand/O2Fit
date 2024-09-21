using System;

namespace Identity.Service.v1.Query.GetUserInformation
{   /// <summary>
    /// نمایش اطلاعات کاربران برای ادمین 
    /// </summary>
    public class GetUsersInformationQueryResult
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }

        public string? FullName { get; set; }
        public string? ImageUri { get; set; }
        public Nullable<DateTime> PkExpireDate { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }


        public int? OS { get; set; }
        public string? Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Market { get; set; }
        public string? AppVersion { get; set; }

    }

    public class GetUsersInformation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public DateTime RegisterDate { get; set; }

    }
    public class GetUserIdByUserNameViewModel
    {
        public int UserId { get; set; }
    }

    public class DeviceInformationViewModel
    {
        public int OS { get; set; }
        public string Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Market { get; set; }
        public string? AppVersion { get; set; }
    }

}
