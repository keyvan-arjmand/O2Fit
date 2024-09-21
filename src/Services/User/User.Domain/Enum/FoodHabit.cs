using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.Domain.Enum
{
    public enum FoodHabit
    {
        [Display(Name = "عادی")]
        Normal=1,

        [Display(Name = "گیاه‌خوار")]
        Vegetarian=2,

        [Display(Name = "خام‌گیاه‌خوار")]
        Vegan=3,

        [Display(Name = "پاک‌گیاه‌خوار")]
        OvoLactoVegetarianism=4
    }
}
