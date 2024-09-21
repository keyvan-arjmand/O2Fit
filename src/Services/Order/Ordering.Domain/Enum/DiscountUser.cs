using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordering.Domain.Enum
{
    public enum DiscountUser
    {
        [Display(Name = "اولین خرید با کد معرف")]
        TenPercent = 10,

        [Display(Name = "اولین خرید با کد معرف")]
        ThirtyPercent = 30,

        [Display(Name = "تخفیف 3 معرف")]
        OneHundredPercent = 100
    }
}
