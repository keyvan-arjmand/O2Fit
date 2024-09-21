using System.ComponentModel.DataAnnotations;

namespace Track.Domain.Enums;

public enum Gender
{
    [Display(Name = "زن")]
    Female = 0, //false 0

    [Display(Name = "مرد")]
    Male = 1    //True 1
}