using System.ComponentModel.DataAnnotations;

namespace Advertise.Domain.Enums;

public enum AdvertiseStatus
{
    [Display(Name = "فعال")]
    Active,
    [Display(Name = "غیر فعال")]
    InActive,
    [Display(Name = "اتمام بودجه")]
    OutOfBudget,
    [Display(Name = "متوقف شده")]
    Paused,
    [Display(Name = "در انتظار تایید")]
    Pending
}