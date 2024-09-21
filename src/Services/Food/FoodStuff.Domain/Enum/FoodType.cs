using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodStuff.Domain.Enum
{
    public enum FoodType
    {

        [Display(Name = "خانگی")]
        Homemade = 1,

        [Display(Name = "رستورانی")]
        Restaurant = 2,

        [Display(Name = "سوپرمارکتی")]
        Supermarket = 3,
    }
}
