using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Payment.Domain.Enums;

public enum PaymentResult
{
    [Display(Name = "در انتظار")]
    Pending = 0,
    [Display(Name = "موفق")]
    Success = 1,
    [Display(Name = "ناموفق")]
    Failed = 2,
}