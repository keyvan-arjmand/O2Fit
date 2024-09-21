namespace Identity.V2.Domain.Enums;

public enum UserStatus
{  
    [Display(Name = "در انتظار تایید")]
    AwaitingConfirmation,
    [Display(Name = "تایید شده")]
    Accepted,
    [Display(Name = "رد شده")]
    Failed,
    [Display(Name = "کاربر عادی")]
    NormalUser
}