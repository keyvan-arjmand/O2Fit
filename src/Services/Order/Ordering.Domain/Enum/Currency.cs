using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Enum
{
    public enum Currency
    {
        [Display(Name = "یورو")] 
        Euro = 0,

        [Display(Name = "تومان")] 
        Toman = 1
    }
}