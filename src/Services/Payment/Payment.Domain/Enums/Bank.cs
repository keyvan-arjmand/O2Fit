using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Payment.Domain.Enums;

public enum Bank
{
    [Display(Name = "بانک ملت")]
    Mellat = 0,

    [Display(Name = "بانک سامان")]
    Saman = 1,

    [Display(Name = "یک پی")]
    YekPay = 2,

    [Display(Name = "تخفیف صد درصدی")]
    Discount = 3,

    [Display(Name = "بازار")]
    CafeBazar = 4,

    [Display(Name = "مایکت")]
    Myket = 5
}