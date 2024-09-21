using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Payment.Domain.Enums;

public enum Currency
{
    [Display(Name = "یورو")]
    Euro = 0,

    [Display(Name = "تومان")]
    Toman = 1
}