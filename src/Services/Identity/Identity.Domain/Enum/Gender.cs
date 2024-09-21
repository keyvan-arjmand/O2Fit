using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Domain.Enum
{
    public enum Gender
    {
        [Display(Name = "زن")]
        Female = 0, //false 0

        [Display(Name = "مرد")]
        Male = 1    //True 1
    }
}
