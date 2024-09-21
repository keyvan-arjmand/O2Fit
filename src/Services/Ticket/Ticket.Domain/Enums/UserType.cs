using System.ComponentModel.DataAnnotations;

namespace Ticket.Domain.Enums;

public enum UserType
{
    [Display(Name = "کاربر")]
    User = 0,
    [Display(Name = "متخصص")]
    Nutritionist = 1,
    [Display(Name = "ادمین")]
    Admin = 2,
}