
using System.ComponentModel.DataAnnotations;


namespace Social.Domain.Enum
{
    public enum Classification
    {
        [Display(Name = "مالی")]
        Finance = 1,

        [Display(Name = "مدیریت")]
        Admin = 2,

        [Display(Name = "انتقاد یا پیشنهادات ")]
        HumanResource = 3,

        [Display(Name = "امور مشتریان")]
        TechnicalSupport = 4,

        [Display(Name = "پیام وب سایت")]
        WebMessage = 5
    }
}
