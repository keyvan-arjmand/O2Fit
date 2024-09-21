using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enums;

public enum ScientificDegree
{
    [Display(Name = "کارشناس")]
    Expert,
    [Display(Name = "کارشناس ارشد")]
    Ma,
    [Display(Name = "دکتری")]
    Phd
}