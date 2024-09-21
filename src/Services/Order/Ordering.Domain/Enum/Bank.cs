using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordering.Domain.Enum
{
    public enum Bank
    {
        [Display(Name = "بانک ملت")]
        Melat,

        [Display(Name = "بانک سامان")]
        Saman,

        [Display(Name = "یک پی")]
        YekPay,

        [Display(Name = "تخفیف صد درصدی")]
        Discount,

        [Display(Name = "بازار")]
        CafeBazar=4,

        [Display(Name = "مایکت")]
        Myket
    }
}
